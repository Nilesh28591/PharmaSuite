using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        ISaleRepo repo;
        public SaleController(ISaleRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("GetMedicines")]
        public IActionResult GetMedicines()
        {
            var data = repo.GetMedicines();
            return Ok(data);

        }


        [HttpPost]
        [Route("AddSales")]
        public IActionResult AddSales(SalesDTO dto)
        {
            int saleId = repo.addSale(dto);

            var response = new SaleResponseDTO
            {
                SaleId = saleId,
               
            };

            return Ok(response);


        }

        [HttpGet]
        [Route("FetchSales")]
        public IActionResult FetchSales()
        {
            var data = repo.sales();
            return Ok(data);
        }

        [HttpGet]
        [Route("getSaleBySaleIdToGenerateInvoice/{id}")]
        public IActionResult generateInvoice(int id)
        {
            var sale = repo.generateInvoice(id);
            var data = new InvoiceDTO()
            {
                SaleId = sale.SaleId,
                CustomerName = sale.CustomerName,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                SaleItems = sale.SaleItems.Select(item => new InvoiceItemDTO
                {
                    Name = item.Medicine.Name,
                    Quantity = item.Quantity,
                    PricePerUnit = item.PricePerUnit,
                    Discount = item.Discount

                }).ToList()
            };
            return Ok(data);
        }

        [HttpGet]
        [Route("getSaleItemByID/{id}")]
        public IActionResult GetSaleItem(int id)
        {
            var data = repo.getItem(id);
            return Ok(data);
        }

        [HttpGet]
        [Route("getQuantity/{id}")]
        public IActionResult getQuantity(int id)
        {
            int quantity = repo.getQuantity(id);
            return Ok(quantity);
        }
        [HttpGet]
        [Route("getUnitPrice/{id}")]
        public IActionResult getUnitPrice(int id)
        {
            double pricePerUnit = repo.getUnitPrice(id);
            return Ok(pricePerUnit);
        }

    }
}
