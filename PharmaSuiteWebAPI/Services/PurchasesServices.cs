using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;
using PharmaSuiteWebAPI.Repo;

namespace PharmaSuiteWebAPI.Services
{
    public class PurchasesServices : IPurchasesRepo
    {
        public PharmaSuiteDBContext _Context;
        public IMapper _mapper;
        public PurchasesServices(PharmaSuiteDBContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task AddPurchasAsyc(PurchaseDTO purchasedto)
        {
            var data = _mapper.Map<Purchase>(purchasedto);
            data.PurchaseDate = DateTime.Now;
            data.CreatedAt = DateTime.Now;
            data.ModifiedBy = purchasedto.CreatedBy;
            data.ModifiedAt = DateTime.Now;

            data.Items = purchasedto.Items.Select(i =>
            {
                var item = _mapper.Map<PurchaseItem>(i);
                item.CreatedAt = DateTime.Now;
                item.ModifiedBy = purchasedto.CreatedBy;
                item.ModifiedAt = DateTime.Now;
                return item;
            }).ToList();

            _Context.purchase.Add(data);
            await _Context.SaveChangesAsync();
        }

        public async Task DeletePurchaseAsync(int id)
        {
            var existingPurchase = await _Context.purchase
        .Include(p => p.Items)
        .FirstOrDefaultAsync(p => p.PurchaseId == id);

            if (existingPurchase == null)
                throw new Exception($"Purchase with Id {id} not found");

            // Remove associated items
            _Context.purchaseItem.RemoveRange(existingPurchase.Items);
            _Context.purchase.Remove(existingPurchase);

            await _Context.SaveChangesAsync();
        }

        public async Task EditPurchase(int id, PurchaseDTO purchasedto)
        {
            var data = await _Context.purchase
         .Include(p => p.Items)
         .FirstOrDefaultAsync(p => p.PurchaseId == id);


            // Update scalar fields
            data.SupplierId = purchasedto.SupplierId;
            data.InvoiceNumber = purchasedto.InvoiceNumber;
            data.ModifiedBy = purchasedto.CreatedBy;
            data.ModifiedAt = DateTime.Now;

            // Remove existing items
            _Context.purchaseItem.RemoveRange(data.Items);

            // Add new items
            data.Items = purchasedto.Items.Select(i =>
            {
                var item = _mapper.Map<PurchaseItem>(i);
                item.CreatedAt = DateTime.Now;
                item.ModifiedBy = purchasedto.CreatedBy;
                item.ModifiedAt = DateTime.Now;
                return item;
            }).ToList();

            await _Context.SaveChangesAsync();
        }

        public async Task<List<Medicine_Management>> GelAllMedicineStockAsync()
        {
            var data= await _Context.Medicine_Managements.ToListAsync();
            return data;
        }

        public async Task<List<PurchaseDTO>> GetAllPurchas()
        {
            var data = await _Context.purchase
                .Include(p => p.Supplier)
                .Include(p => p.Items).ThenInclude(i => i.Medicine)
                .OrderByDescending(p => p.PurchaseDate)
                .ToListAsync();

            return _mapper.Map<List<PurchaseDTO>>(data);
        }

        public async Task<IEnumerable<Supplier>> GetAllSupplier()
        {
            var data = await _Context.supplier.ToListAsync();
            return data;
        }

        public async Task<PurchaseDTO> GetPurchaseById(int id)
        {
           var data=await _Context.purchase.Include(x=>x.Supplier).Include(x=>x.Items).ThenInclude(x=>x.Medicine).FirstOrDefaultAsync(x=>x.PurchaseId==id);
            if (data == null)
            {
                return null;
            }
            return _mapper.Map<PurchaseDTO>(data);
        }
    }
}
