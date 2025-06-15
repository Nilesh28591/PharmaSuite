using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        public IPurchasesRepo _purchasesRepo;
        public PurchaseController(IPurchasesRepo purchasesRepo)
        {
            _purchasesRepo = purchasesRepo;   
        }

        [HttpPost]
        [Route("AddPurchase")]
        public async Task<IActionResult> AddPurchase([FromBody] PurchaseDTO purchaseDTO)
        {
            await _purchasesRepo.AddPurchasAsyc(purchaseDTO);
            return Ok("Purchase Added Succesfully");
        }

        [HttpGet]
        [Route("GetAllPurchase")]
        public async Task<IActionResult> GetAllPurchase()
        {
            var data=await _purchasesRepo.GetAllPurchas();
            return Ok(data);
        }
        [HttpGet]
        [Route("GetMedcinestock")]
        public async Task<IActionResult> GetMedicineStock()
        {
            var data = await _purchasesRepo.GelAllMedicineStockAsync();
            return Ok(data);
        }
    }
}
