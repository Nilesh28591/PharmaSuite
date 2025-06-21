using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        PharmaSuiteDBContext db;
        IMapper mapper;

        public MedicineController(PharmaSuiteDBContext db , IMapper mapper) { this.db = db; this.mapper = mapper; }


        [HttpGet]
        [Route("StockAlert")]
        public IActionResult stockAlert() 
        {
                int lowStockCount = db.purchaseItem
                      .Where(x => x.Quantity <= x.MinQuantity)
                      .Count();
            return Ok(lowStockCount);
        }

        [HttpGet]
        [Route("ExpAlert")]
        public IActionResult ExpAlert()
        {
            int lowStockCount = db.purchaseItem.Where(x => x.ExpiryDate <= DateTime.Now).Count();
            return Ok(lowStockCount);
        }

        [HttpGet]
        [Route("PriorExpAlert")]
        public IActionResult PriorExpAlert()
        {
            DateTime Days30 = DateTime.Now + TimeSpan.FromDays(30);

            int lowStockCount = db.purchaseItem.Where(x => x.ExpiryDate <= Days30 && x.ExpiryDate>= DateTime.Now).Count();
            return Ok(lowStockCount);
        }


        [HttpGet]
        [Route("StockAlertTable")]
        public IActionResult stockAlertTable()
        {
            var lowStockCount = db.purchaseItem.Where(x => x.Quantity <= x.MinQuantity).Include(x=>x.Medicine).Select(x=> new PurchaseItemDtoSF() 
            {
                PurchaseItemId = x.PurchaseItemId,
                MedicineId = x.MedicineId,
                MedicineName = x.Medicine.Name,
                BatchNo = x.BatchNo,
                MfgDate = x.MfgDate,
                ExpiryDate = x.ExpiryDate,
                Quantity = x.Quantity,
                CostPrice = x.CostPrice,
                MinQuantity = x.MinQuantity

            }).ToList();
            return Ok(lowStockCount);
        }


        [HttpGet]
        [Route("ExpAlertTable")]
        public IActionResult ExpAlertTable()
        {
            var lowStockCount = db.purchaseItem.Where(x => x.ExpiryDate <= DateTime.Now).Include(x => x.Medicine).Select(x => new PurchaseItemDtoSF()
            {
                PurchaseItemId = x.PurchaseItemId,
                MedicineId = x.MedicineId,
                MedicineName = x.Medicine.Name,
                BatchNo = x.BatchNo,
                MfgDate = x.MfgDate,
                ExpiryDate = x.ExpiryDate,
                Quantity = x.Quantity,
                CostPrice = x.CostPrice,
                MinQuantity = x.MinQuantity

            }).ToList();
            return Ok(lowStockCount);
        }

        [HttpGet]
        [Route("TodaySale")]
        public IActionResult TodaySaleTable() 
        {
           var total_sale =  db.SaleItems.Include(x => x.Sale).Include(x => x.Medicine).Where(x=> x.Sale.SaleDate.Date.Equals(DateTime.Today)).Select(x=> new TodaySaleDto() 
            {
                CustomerName = x.Sale.CustomerName,
                SaleDate = x.Sale.SaleDate,
                Quantity = x.Quantity,
                Discount = x.Discount,
                TotalAmount = x.Sale.TotalAmount,
                MedicineName = x.Medicine.Name
                
            }).ToList();
            return Ok(total_sale);
        }
        [HttpGet]
        [Route("Top5")]
        public IActionResult Top5()
        {
            var total_sale = db.SaleItems.Include(x => x.Medicine).GroupBy(x=>x.Medicine.Name).Select(y => new Top5Dto()
            {
               
                Quantity = y.Sum(x=>x.Quantity),
                
                MedicineName = y.Key

            }).OrderByDescending(x => x.Quantity).Take(5).ToList();
            return Ok(total_sale);
        }

        [HttpGet]
        [Route("PriorExpAlertTable")]
        public IActionResult PriorExpAlertTable()
        {
            DateTime Days30 = DateTime.Now + TimeSpan.FromDays(30);

            var lowStockCount = db.purchaseItem.Where(x => x.ExpiryDate <= Days30 && x.ExpiryDate > DateTime.Now).Include(x => x.Medicine).Select(x => new PurchaseItemDtoSF()
            {
                PurchaseItemId = x.PurchaseItemId,
                MedicineId = x.MedicineId,
                MedicineName = x.Medicine.Name,
                BatchNo = x.BatchNo,
                MfgDate = x.MfgDate,
                ExpiryDate = x.ExpiryDate,
                Quantity = x.Quantity,
                CostPrice = x.CostPrice,
                MinQuantity = x.MinQuantity

            }).ToList();
            return Ok(lowStockCount);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add_Medicine(Medicine_Dto Dto)
        {
            var medicine = new Medicine_Management()
            {
                MedicineId = Dto.MedicineId,
                Name = Dto.Name,
                Category = Dto.Category,
                Manufacturer = Dto.Manufacturer,
                PricePerUnit = Dto.PricePerUnit,
                ExpiryDate = Dto.ExpiryDate,
                BatchNo = Dto.BatchNo,
            };
            db.Medicine_Managements.Add(medicine);
            db.SaveChanges();
            return Ok("success");
        }

        [HttpGet]
        [Route("Fetch")]
        public IActionResult Fetch_Medicine()
        {
            var list = db.Medicine_Managements.ToList();
            return Ok(list);
        }


        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit_Medicine(int id)
        {
            var list = db.Medicine_Managements.Find(id);
            return Ok(list);

        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult Delete_Medicine(int id)
        {
            var list = db.Medicine_Managements.Find(id);
            db.Medicine_Managements.Remove(list);
            db.SaveChanges();

            return Ok("success");
        }



        [HttpPut]
        [Route("PostEdit")]
        public IActionResult Edit_Medicine(Medicine_Dto dto)
        {
            var list = mapper.Map<Medicine_Management>(dto);
            //var list = new Medicine_Management()
            //{
            //    MedicineId = dto.MedicineId
            //};
            db.Medicine_Managements.Update(list);
            db.SaveChanges();
            return Ok("success");
        }

        [HttpPost]
        [Route("AddCat")]
        public IActionResult Add_Cat(CategoryDto_Add Dto)
        {
            var medicine = new Category()
            {
                CatId = Dto.CatId,
                CategoryName = Dto.CategoryName,
                CreatedAt = Dto.CreatedAt,
                CreatedBy = Dto.CreatedBy,
                Status = Dto.Status,
            };
            db.Medicine_categories.Add(medicine);
            db.SaveChanges();
            return Ok("success");
        }

        [HttpGet]
        [Route("FetchCat")]
        public IActionResult Fetch_Categories()
        {
            var list = db.Medicine_categories.Where(x => x.Status.Equals("Active")).ToList();
            return Ok(list);
        }


        [HttpPost]
        [Route("AddMfg")]
        public IActionResult Add_Mfg(Manifacturer_Dto_A Dto)
        {
            var medicine = new Manifacturer_Medicine()
            {
                ManId = Dto.ManId,
                MName = Dto.MName,
                CreatedAt = Dto.CreatedAt,
                CreatedBy = Dto.CreatedBy,
                Status = Dto.Status,
            };
            db.Medicine_Manifacturer.Add(medicine);
            db.SaveChanges();
            return Ok("success");
        }

        [HttpGet]
        [Route("FetchMfg")]
        public IActionResult Fetch_Mfg()
        {
            var list = db.Medicine_Manifacturer.Where(x => x.Status.Equals("Active")).ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("Sumsales")]
        public IActionResult SumSales()
        {
            double list = db.Sales.Sum(x => x.TotalAmount);
            return Ok(list);
        }

        [HttpGet]
        [Route("SumMedicines")]
        public IActionResult SumMeds()
        {
            double list = db.purchaseItem.Sum(x => x.Quantity);
            return Ok(list);
        }

        

        [HttpGet]
        [Route("MonthGraph")]
        public IActionResult MonthGraph()
        {
            var list = db.Sales
    .GroupBy(s => s.SaleDate.Month)
    .Select(g => new
    {
        Month = g.Key,
        Total = g.Sum(s => s.TotalAmount)
    })
    .OrderBy(x => x.Month)
    .ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("YearGraph")]
        public IActionResult YearGraphx ()
        {
            var list = db.Sales
    .GroupBy(s => s.SaleDate.Year)
    .Select(g => new
    {
        Month = g.Key,
        Total = g.Sum(s => s.TotalAmount)
    })
    .OrderBy(x => x.Month)
    .ToList();
            return Ok(list);
        }

    }
}
