using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaSuiteMVC.Models;
using System.Net.Security;

namespace PharmaSuiteMVC.Controllers
{
    public class PurchaseController : Controller
    {
        HttpClient _client;
        public readonly string? _baseUrl;
        public PurchaseController(IConfiguration configuration)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (SslPolicyErrors, chain, cert, Sender) => { return true; };
            _client = new HttpClient(handler);
            _baseUrl = configuration["ApiSettings:BaseUrl"];
        }
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    List<Purchase> items = new List<Purchase>();
        //    string url = $"{_baseUrl}/api/Purchase/GetAllPurchase";
        //    HttpResponseMessage response = _client.GetAsync(url).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json=response.Content.ReadAsStringAsync().Result;
        //        var obj = JsonConvert.DeserializeObject<List<Purchase>>(json);
        //        if (obj != null)
        //        {
        //            items=obj;
        //        }
        //    }
        //    return View(items);
        // }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllPurchases()
        {
            var response = _client.GetAsync($"{_baseUrl}/api/Purchase/GetAllPurchase").Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var purchases = JsonConvert.DeserializeObject<List<Purchase>>(json) ?? new List<Purchase>();
            return Json(purchases);
        }
        [HttpGet]
        public IActionResult AddPurchaseAsync()
        {
            return View();
        }
        [HttpPost("/Purchase/AddPurchaseAsync")]
        public IActionResult AddPurchaseAsync([FromBody] Purchase purchase)
        {
            string url = $"{_baseUrl}/api/Purchase/AddPurchase";
            var content = new StringContent(JsonConvert.SerializeObject(purchase), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            List<SupplierDTO> suppliers = new List<SupplierDTO>();
            var response = _client.GetAsync($"{_baseUrl}/api/Purchase/GetSupplier").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(json) ?? new List<SupplierDTO>();
            }

            return Json(suppliers);
        }

        [HttpGet]
        public IActionResult GetAllMedicines()
        {
            List<Medicine_Management> medicines = new List<Medicine_Management>();
            var response = _client.GetAsync($"{_baseUrl}/api/Purchase/GetMedcineStock").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                medicines = JsonConvert.DeserializeObject<List<Medicine_Management>>(json) ?? new List<Medicine_Management>();
            }

            return Json(medicines);
        }
        [HttpGet("/Purchase/GetPurchaseById")]
        public IActionResult GetPurchaseById(int id)
        {
            var response = _client.GetAsync($"{_baseUrl}/api/Purchase/GetPurchaseById/{id}").Result;

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }

            var json = response.Content.ReadAsStringAsync().Result;
            var purchase = JsonConvert.DeserializeObject<Purchase>(json);
            return Json(purchase);
        }

        [HttpDelete("/Purchase/DeletePurchase")]
        public IActionResult DeletePurchase(int id)
        {
            var url = $"{_baseUrl}/api/Purchase/DeletePurchase/{id}";
            var response=_client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return StatusCode((int)response.StatusCode,response.ReasonPhrase);
        }
        [HttpPut("/Purchase/EditPurchase")]
        public IActionResult UpdatePurcase(int id, [FromBody] Purchase updatedDto)
        {
            var url = $"{_baseUrl}/api/Purchase/EditPurchase/{id}";
            var content = new StringContent(JsonConvert.SerializeObject(updatedDto), System.Text.Encoding.UTF8, "application/json");

            var response = _client.PutAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }
    }
}
