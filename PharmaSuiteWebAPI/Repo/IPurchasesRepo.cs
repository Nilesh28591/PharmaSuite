using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Repo
{
    public interface IPurchasesRepo
    {
        Task AddPurchasAsyc(PurchaseDTO purchasedto);
        Task<List<PurchaseDTO>> GetAllPurchas();
        Task<IEnumerable<Supplier>> GetAllSupplier();

        Task<List<Medicine_Management>> GelAllMedicineStockAsync();

        Task<PurchaseDTO> GetPurchaseById(int id);

        Task EditPurchase(int id,PurchaseDTO purchasedto);

        Task DeletePurchaseAsync(int id);
    }
}
