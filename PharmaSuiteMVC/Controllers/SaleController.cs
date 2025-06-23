using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PharmaSuiteMVC.Models;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using Document = iTextSharp.text.Document;
namespace PharmaSuiteMVC.Controllers
{
    public class SaleController : Controller
    {
        HttpClient client;
        IWebHostEnvironment env;
        
        public SaleController(IWebHostEnvironment env)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
            this.env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sale()
        {
            List<MedicineDTO> data = new List<MedicineDTO>();
            string url = "https://localhost:7259/api/Sales/GetMedicines/";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<MedicineDTO>>(jsondata);
                if (obj != null)
                {
                    data = obj;
                }
            }
            ViewBag.GetMedicinesUnitPrice = JsonConvert.SerializeObject(data);
            ViewBag.GetAllMedicine = new SelectList(data, "MedicineId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Sale([FromBody] SaleDTO sale)
        {
            string url = "https://localhost:7259/api/Sales/AddSales/";
            var json = JsonConvert.SerializeObject(sale);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync().Result;

                // ✅ Use var with strongly typed class
                var result = JsonConvert.DeserializeObject<SaleResponseDTO>(responseJson);

                var saleId = result.SaleId;

                //return RedirectToAction("generateInvoice", new { id = saleId });
                var redirectUrl = Url.Action("generateInvoice", new { id = saleId });
                return Json(new { success = true, redirectUrl });


            }
            PopulateMedicines();
            return View(sale);
        }

        public IActionResult getSalesData()
        {
            List<SaleDTO> data = new List<SaleDTO>();
            string url = "https://localhost:7259/api/Sales/FetchSales/";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<SaleDTO>>(jsondata);
                if (obj != null)
                {
                    data = obj;
                }
            }

            return View(data);

        }
        public IActionResult generateInvoice(int id)
        {
            InvoiceDTO invoice = new InvoiceDTO();
            string url = $"https://localhost:7259/api/Sales/getSaleBySaleIdToGenerateInvoice/{id}";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<InvoiceDTO>(jsondata);
                if (obj != null)
                {
                    invoice = obj;
                }
            }




            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 36, 36, 72, 72);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Fonts
                var titleFont = FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                var labelFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
                var valueFont = FontFactory.GetFont("Arial", 12);

                // Header
                Paragraph header = new Paragraph("PharmaSuit Medical Store", titleFont);
                header.Alignment = Element.ALIGN_CENTER;
                doc.Add(header);
                doc.Add(new Paragraph("123 Health Street, Wellness City, India", valueFont) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Paragraph("Phone: +91-9876543210 | Email: support@pharmasuit.com", valueFont) { Alignment = Element.ALIGN_CENTER });
                doc.Add(new Chunk("\n"));

                // Invoice Info Table
                PdfPTable invoiceTable = new PdfPTable(2);
                invoiceTable.WidthPercentage = 100;
                invoiceTable.SetWidths(new float[] { 30, 70 });

                invoiceTable.AddCell(GetCell("Invoice ID:", labelFont));
                invoiceTable.AddCell(GetCell(invoice.SaleId.ToString(), valueFont));

                invoiceTable.AddCell(GetCell("Customer Name:", labelFont));
                invoiceTable.AddCell(GetCell(invoice.CustomerName, valueFont));

                invoiceTable.AddCell(GetCell("Date:", labelFont));
                invoiceTable.AddCell(GetCell(invoice.SaleDate.ToString("dd-MM-yyyy"), valueFont));

                invoiceTable.AddCell(GetCell("Total Amount:", labelFont));
                invoiceTable.AddCell(GetCell($"₹{invoice.TotalAmount}", valueFont));

                doc.Add(invoiceTable);
                doc.Add(new Chunk("\n"));

                // Item Table
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 10, 40, 15, 15, 20 });

                AddHeaderCell(table, "S.No");
                AddHeaderCell(table, "Medicine Name");
                AddHeaderCell(table, "Quantity");
                AddHeaderCell(table, "Unit Price");
                AddHeaderCell(table, "Discount");

                int count = 1;
                foreach (var item in invoice.SaleItems)
                {
                    table.AddCell(count.ToString());
                    table.AddCell(item.Name);
                    table.AddCell(item.Quantity.ToString());
                    table.AddCell($"₹{item.UnitPrice}");
                    table.AddCell(item.Discount.HasValue ? $"{item.Discount}%" : "0%");
                    count++;
                }

                doc.Add(table);

                // Footer
                doc.Add(new Chunk("\n"));
                Paragraph footer = new Paragraph("Thank you for choosing PharmaSuit. Stay Healthy!", valueFont);
                footer.Alignment = Element.ALIGN_CENTER;
                doc.Add(footer);

                doc.Close();

                // Save to Content/invoices
                string contentPath = Path.Combine(env.WebRootPath, "Content", "invoices");
                if (!Directory.Exists(contentPath))
                    Directory.CreateDirectory(contentPath);

                string fileName = $"Invoice_{invoice.SaleId}.pdf";
                string filePath = Path.Combine(contentPath, fileName);
                System.IO.File.WriteAllBytes(filePath, ms.ToArray());

                return File(ms.ToArray(), "application/pdf", fileName);
            }
        }

        public IActionResult getSaleItems(int id)
        {
            List<Items> saleitems = new List<Items>();
            string url = $"https://localhost:7259/api/Sales/getSaleItemByID/{id}";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Items>>(jsondata);
                if (obj != null)
                {
                    saleitems = obj;
                    return new JsonResult(saleitems);

                }

            }
            return View();
        }

        public IActionResult getQuantity(int id)
        {
            string url = $"https://localhost:7259/api/Sales/getQuantity/{id}";

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;

                int quantity = JsonConvert.DeserializeObject<int>(jsonData);

                return Json(quantity);
            }
            return View();

        }
        public IActionResult getUnitPrice(int id)
        {
            string url = $"https://localhost:7259/api/Sales/getUnitPrice/{id}";

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;

                double unitPrice = JsonConvert.DeserializeObject<double>(jsonData);

                return Json(unitPrice);
            }
            return View();
        }
        private void PopulateMedicines()
        {
            List<MedicineDTO> data = new List<MedicineDTO>();
            string url = "https://localhost:7259/api/Sales/GetMedicines/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<MedicineDTO>>(jsondata);
                if (obj != null)
                {
                    data = obj;
                }
            }

            ViewBag.GetAllMedicine = new SelectList(data, "MedicineId", "Name");
            ViewBag.GetMedicinesUnitPrice = JsonConvert.SerializeObject(data.Select(m => new
            {
                medicineId = m.MedicineId,
                unitPrice = m.PricePerUnit
            }));

        }
        private PdfPCell GetCell(string text, iTextSharp.text.Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Border = Rectangle.NO_BORDER;
            cell.Padding = 5;
            return cell;
        }

        private void AddHeaderCell(PdfPTable table, string text)
        {
            var font = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.BackgroundColor = new BaseColor(0, 102, 204); // Navy Blue
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 5;
            table.AddCell(cell);
        }



    }
}
