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
        [Route("GetMedcineStock")]
        public async Task<IActionResult> GetMedicineStock()
        {
            var data = await _purchasesRepo.GelAllMedicineStockAsync();
            return Ok(data.Select(m => new {m.MedicineId,m.Name,m.BatchNo,m.ExpiryDate,m.Category,m.Manufacturer,m.PricePerUnit}));
        }
        [HttpGet]
        [Route("GetSupplier")]
        public async Task<IActionResult> GetAllSupplier()
        {
            var data = await _purchasesRepo.GetAllSupplier();
            return Ok(data.Select(s => new {s.SupplierId,s.Name,s.ContactPerson,s.Email,s.Address,s.Phone}));
        }
        [HttpGet("GetPurchaseById/{id}")]
        public async Task<IActionResult> GetPurchaseById(int id)
        {
            var data=await _purchasesRepo.GetPurchaseById(id);
            return Ok(data);
        }
        [HttpDelete("DeletePurchase/{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
                await _purchasesRepo.DeletePurchaseAsync(id);
                return Ok(new { Message = "Deleted successfully" });
        }
        [HttpPut("EditPurchase/{id}")]
        public async Task<IActionResult> EditPurchase(int id, [FromBody] PurchaseDTO updatedDto)
        {
            await _purchasesRepo.EditPurchase(id, updatedDto);
            return Ok(new { Message = "Updated Succesfully" });
        }
    }
}
