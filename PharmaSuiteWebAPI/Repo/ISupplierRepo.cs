using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Repo
{
    public interface ISupplierRepo
    {
        void AddSupplier(SupplierDTO supplier);

        List<Supplier> GetSupplier();

        bool DeleteSup(int id);

        bool EditSupplier(SupplierDTO supplier);

        Supplier GetSupbyId(int id);
    }
}
