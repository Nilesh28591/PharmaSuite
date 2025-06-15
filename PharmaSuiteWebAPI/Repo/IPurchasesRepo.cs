using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Repo
{
    public interface IPurchasesRepo
    {
        Task AddPurchasAsyc(PurchaseDTO purchasedto);
        Task<List<PurchaseDTO>> GetAllPurchas();

        Task<List<MedicineStockDTO>> GelAllMedicineStockAsync();
    }
}
