using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Repo
{
    public interface ISaleRepo
    {
        List<AvailableMedicineDTO> GetMedicines();
        int addSale(SalesDTO dto);
        List<Sale> sales();
        Sale generateInvoice(int id);
        List<ItemsDTO> getItem(int id);
        int getQuantity(int id);
        double getUnitPrice(int id);
    }
}
