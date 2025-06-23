using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Services
{
    public class SaleService:ISaleRepo
    {
        PharmaSuiteDBContext db;
        public SaleService(PharmaSuiteDBContext db)
        {
           this.db = db; 
        }
        public List<AvailableMedicineDTO> GetMedicines()
        {
            // ye wala se pura date fetch kar raha hu where join m in db.Medicines on pi.MedicineId equals m.MedicineId
            var Data1 = (
                from pi in db.purchaseItem
                join m in db.Medicine_Managements on pi.MedicineId equals m.MedicineId
                where pi.Quantity > 0 && DateTime.Now < pi.ExpiryDate
                select new
                {
                    MedicineId = m.MedicineId,
                    Name = m.Name,
                    Category = m.Category,
                    Manufacturer = m.Manufacturer,
                    PricePerUnit = m.PricePerUnit,
                    BatchNo = pi.BatchNo,
                    ExpiryDate = pi.ExpiryDate,
                    Quantity = pi.Quantity,
                    CostPrice = pi.CostPrice
                }).ToList();


            // fir isme same name wale medicine ko ek hi baar dikha raha hu
            var Data2 = Data1
                .GroupBy(x => x.MedicineId)
                .Select(g => g.OrderBy(x => x.ExpiryDate).First())
                .Select(x => new AvailableMedicineDTO
                {
                    MedicineId = x.MedicineId,
                    Name = x.Name,
                    Category = x.Category,
                    Manufacturer = x.Manufacturer,
                    PricePerUnit = x.PricePerUnit,
                    BatchNo = x.BatchNo,
                    ExpiryDate = x.ExpiryDate,
                    Quantity = x.Quantity,
                   
                }).ToList();


            return Data2;
        }
        public int addSale(SalesDTO saleDto)
        {
            var sale = new Sale()
            {
                CustomerName = saleDto.CustomerName,
                SaleDate = saleDto.SaleDate,
                TotalAmount = saleDto.TotalAmount,
                SaleItems = saleDto.SaleItems.Select(item => new SaleItem
                {
                    MedicineId = item.MedicineId,
                    Quantity = item.Quantity,
                    PricePerUnit = item.PricePerUnit,
                    Discount = item.Discount,

                }).ToList()
            };

            db.Sales.Add(sale);
            db.SaveChanges();

            foreach (var items in sale.SaleItems)
            {
                int medicineId = items.MedicineId;
                int purchasedQuantity = items.Quantity;
                int totalAvailable = db.purchaseItem
                                     .Where(x => x.MedicineId == medicineId && x.Quantity > 0)
                                     .Sum(x => x.Quantity);

                if (totalAvailable < purchasedQuantity)
                {
                    throw new Exception($"Insufficient stock for MedicineId: {medicineId}. Required: {purchasedQuantity}, Available: {totalAvailable}");
                }

                var purchaseItems = db.purchaseItem
                                     .Where(x => x.MedicineId == medicineId && x.Quantity > 0)
                                     .OrderBy(x => x.ExpiryDate)
                                     .ToList();

                foreach (var pi in purchaseItems)
                {
                    if (purchasedQuantity <= 0)
                        break;

                    if (pi.Quantity >= purchasedQuantity)
                    {
                        pi.Quantity -= purchasedQuantity;
                        purchasedQuantity = 0;
                    }
                    else
                    {
                        purchasedQuantity -= pi.Quantity;
                        pi.Quantity = 0;
                    }
                }

                db.SaveChanges();
            }
            return sale.SaleId;
        }
        public List<Sale> sales()
        {
            var data = db.Sales.ToList();
            return data;
        }
        public Sale generateInvoice(int id)
        {
            var data = db.Sales
                         .Include(s => s.SaleItems)
                             .ThenInclude(si => si.Medicine)
                         .FirstOrDefault(s => s.SaleId == id);
            return data;
        }

        public List<ItemsDTO> getItem(int id)
        {
            var data = db.SaleItems.Where(x => x.SaleId == id).Include(x => x.Medicine).Select(x => new ItemsDTO()
            {
                ItemId = x.ItemId,
                SaleId = x.SaleId,
                MedicineId = x.MedicineId,
                Name = x.Medicine.Name,
                Quantity = x.Quantity,
                PricePerUnit = x.PricePerUnit,
                Discount = x.Discount
            }).ToList();

            return data;
        }
        public int getQuantity(int id)
        {
            var data = db.purchaseItem.Where(x => x.MedicineId == id).ToList();
            int quantity = 0;
            foreach (var item in data)
            {
                quantity = quantity + item.Quantity;
            }
            return quantity;
        }
        public double getUnitPrice(int id)
        {
            var data = db.Medicine_Managements.FirstOrDefault(x => x.MedicineId == id);
            double pricePerUnit = data.PricePerUnit;
            return pricePerUnit;
        }
        public List<Customer> GetCustomers()
        {
            var data = db.Customers.ToList();
            return data;

        }
        public void addCustomer(CustomerDTO dto)
        {
            var data = new Customer()
            {
                Name = dto.Name,
                Mobile = dto.Mobile,    // use Mobile to match Model
                Email = dto.Email,
                Address = dto.Address
            };
            db.Customers.Add(data);
            db.SaveChanges();
        }


    }
}
