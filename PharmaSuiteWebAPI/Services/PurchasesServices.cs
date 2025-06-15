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
            var data=_mapper.Map<Purchase>(purchasedto);
            data.PurchaseDate=DateTime.Now;
            data.CreatedAt = DateTime.Now;
            data.ModifiedBy = purchasedto.CreatedBy; // <-- Fix
            data.ModifiedAt = DateTime.Now;
            data.Items = purchasedto.Items.Select(i =>
            {
                var items = _mapper.Map<PurchaseItem>(i);
                items.CreatedAt = DateTime.Now;
                items.CreatedAt = DateTime.Now;
                items.ModifiedBy = purchasedto.CreatedBy; // <-- Fix
                items.ModifiedAt = DateTime.Now;
                return items;
            }).ToList();
           _Context.purchase.Add(data);
            await _Context.SaveChangesAsync();

        }

        public Task<List<MedicineStockDTO>> GelAllMedicineStockAsync()
        {
            var data = _Context.purchaseItem.GroupBy(i => new { i.MedicineId, i.Medicine.Name }).
                      Select(x => new MedicineStockDTO
                      {
                          MedicineId=x.Key.MedicineId,
                          Name=x.Key.Name,
                          TotalQuantity=x.Sum(x=>x.Quantity)

                      }).ToListAsync();
            return data;
        }

        public async Task<List<PurchaseDTO>> GetAllPurchas()
        {
           var data = await _Context.purchase
        .Include(p => p.Supplier)
        .Include(p => p.Items)
            .ThenInclude(i => i.Medicine)
        .OrderByDescending(p => p.PurchaseDate)
        .ToListAsync();

             return _mapper.Map<List<PurchaseDTO>>(data);
        }
    }
}
