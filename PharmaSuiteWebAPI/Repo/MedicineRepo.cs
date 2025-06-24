using AutoMapper;
using PharmaSuiteWebAPI.Data;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Repo
{
    public interface MedicineRepo
    {
        int StockAlertCount();
        int ExpAlert();
        int PriorExpAlert();
        List<PurchaseItemDtoSF> ExpAlertTable();
        List<PurchaseItemDtoSF> StockAlertTable();
        List<PurchaseItemDtoSF> PriorExpAlertTable();
        void Add_Medicine(Medicine_Dto Dto);
        List<Medicine_Management> Fetch_Medicine();
        Medicine_Management Edit_Medicine(int id);
        void Delete_Medicine(Medicine_Management list);
        Medicine_Management Particular_Edit_Medicine(Medicine_Dto dto);
        void Update_Medicine(Medicine_Management list);

        void  Add_Cat(CategoryDto_Add Dto);
        List <Category> Fetch_Cat();
        void Add_mfg(Manifacturer_Dto_A Dto);

        List<Manifacturer_Medicine> Fetch_Mfg();
        double Sumsales();
        double SumMedicines();
        //List<Sale> MonthGraph();

    }
}
