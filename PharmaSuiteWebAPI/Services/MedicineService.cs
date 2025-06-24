using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Services
{
    public class MedicineService : MedicineRepo
    {
        PharmaSuiteDBContext db;
        IMapper mapper;
        public MedicineService(PharmaSuiteDBContext db, IMapper mapper) { this.db = db; this.mapper = mapper; }

        public int StockAlertCount()
        {
            return db.purchaseItem
                      .Where(x => x.Quantity <= x.MinQuantity)
                      .Count();
        }
        public int ExpAlert()
        {
            return db.purchaseItem.Where(x => x.ExpiryDate <= DateTime.Now).Count();
        }

        public int PriorExpAlert()
        {
            DateTime Days30 = DateTime.Now + TimeSpan.FromDays(30);
            return db.purchaseItem.Where(x => x.ExpiryDate <= Days30 && x.ExpiryDate >= DateTime.Now).Count();
        }
        public List<PurchaseItemDtoSF> ExpAlertTable()
        {
            var lowStockCounta = db.purchaseItem.Where(x => x.ExpiryDate <= DateTime.Now).Include(x => x.Medicine).ToList();
            return mapper.Map<List<PurchaseItemDtoSF>>(lowStockCounta);
        }
        public List<PurchaseItemDtoSF> StockAlertTable()
        {
            var lowStockCounta = db.purchaseItem.Where(x => x.Quantity <= x.MinQuantity).Include(x => x.Medicine).ToList();

            return mapper.Map<List<PurchaseItemDtoSF>>(lowStockCounta);

        }
        public List<PurchaseItemDtoSF> PriorExpAlertTable()
        {
            DateTime Days30 = DateTime.Now + TimeSpan.FromDays(30);

            var lowStockCounta = db.purchaseItem.Where(x => x.ExpiryDate <= Days30 && x.ExpiryDate > DateTime.Now).Include(x => x.Medicine).ToList();
            return mapper.Map<List<PurchaseItemDtoSF>>(lowStockCounta);
        }

        public void Add_Medicine(Medicine_Dto Dto)
        {
            var medicine = mapper.Map<Medicine_Management>(Dto);
            //var medicine = new Medicine_Management()
            //{
            //    MedicineId = Dto.MedicineId,
            //    Name = Dto.Name,
            //    Category = Dto.Category,
            //    Manufacturer = Dto.Manufacturer,
            //    PricePerUnit = Dto.PricePerUnit,
            //    ExpiryDate = Dto.ExpiryDate,
            //    BatchNo = Dto.BatchNo,
            //};
            db.Medicine_Managements.Add(medicine);
            db.SaveChanges();
        }
        public List<Medicine_Management> Fetch_Medicine() 
        {
            return db.Medicine_Managements.ToList();
        }
        public Medicine_Management Edit_Medicine(int id)
        {
            return db.Medicine_Managements.Find(id);
        }
        public void Delete_Medicine(Medicine_Management list)
        {
            db.Medicine_Managements.Remove(list);
            db.SaveChanges();
        }
        public Medicine_Management Particular_Edit_Medicine(Medicine_Dto dto) 
        {
            return mapper.Map<Medicine_Management>(dto);
        }
        public void Update_Medicine(Medicine_Management list)
        {
            db.Medicine_Managements.Update(list);
            db.SaveChanges();
        }
        public void Add_Cat(CategoryDto_Add Dto) 
        {
            var medicine = mapper.Map<Category>(Dto);           
            db.Medicine_categories.Add(medicine);
            db.SaveChanges();
        }
        public List<Category> Fetch_Cat()
        {
            return db.Medicine_categories.Where(x => x.Status.Equals("Active")).ToList();
        }

        public void Add_mfg(Manifacturer_Dto_A Dto)
        {
            var medicine = mapper.Map<Manifacturer_Medicine>(Dto);
            db.Medicine_Manifacturer.Add(medicine);
            db.SaveChanges();
        }
        public List<Manifacturer_Medicine> Fetch_Mfg()
        {
            return db.Medicine_Manifacturer.Where(x => x.Status.Equals("Active")).ToList();
        }
        public double Sumsales()
        {
            return db.Sales.Sum(x => x.TotalAmount);
        }

        public double SumMedicines() 
        {
            return db.purchaseItem.Sum(x => x.Quantity);
        }
    //    public List<Sale> MonthGraph()
    //    {
    //        return db.Sales
    //.GroupBy(s => s.SaleDate.Month)
    //.Select(g => new
    //{
    //    Month = g.Key,
    //    Total = g.Sum(s => s.TotalAmount)
    //})
    //.OrderBy(x => x.Month)
    //.ToList();
    //    }






    }
}
