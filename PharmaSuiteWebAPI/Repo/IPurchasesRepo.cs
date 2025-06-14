using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Repo
{
    public interface IPurchasesRepo
    {
        Task AddPurchasAsyc(Purchase purchase);
        Task<List<Purchase>> GetAllPurchas();   
    }
}
