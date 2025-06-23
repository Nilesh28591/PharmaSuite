using AutoMapper;
using PharmaSuiteWebAPI.Dto;
using PharmaSuiteWebAPI.Model;

namespace PharmaSuiteWebAPI.Data
{
    public class MappingData:Profile
    {
        public MappingData()
        {
            CreateMap<PurchaseDTO, Purchase>();
            CreateMap<PurchaseItemDTO, PurchaseItem>();

            CreateMap<ExpenseDto, Expense>().ReverseMap();
            // From Entity to DTO (for Get/Read)
            CreateMap<Purchase, PurchaseDTO>() // Model -> DTO
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Supplier.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Supplier.Email))
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<Medicine_Management, Medicine_Dto>().ReverseMap();
            CreateMap<CategoryDto_Add, Category>().ReverseMap();
            CreateMap<Manifacturer_Dto_A, Manifacturer_Medicine>().ReverseMap();
            CreateMap<PurchaseItem, PurchaseItemDtoSF>().ForMember(x=>x.MedicineName,x=>x.MapFrom(x=>x.Medicine.Name!=null?x.Medicine.Name:"No"));
            CreateMap<SaleItem, TodaySaleDto>()
           .ForMember(x => x.CustomerName, x => x.MapFrom(x => x.Sale.CustomerName != null ? x.Sale.CustomerName : "No"))
           .ForMember(x => x.SaleDate, x => x.MapFrom(x => x.Sale.SaleDate))
           .ForMember(x => x.TotalAmount, x => x.MapFrom(x => x.Sale.TotalAmount))
           .ForMember(x => x.MedicineName, x => x.MapFrom(x => x.Medicine.Name != null ? x.Medicine.Name : "No"));





            CreateMap<PurchaseItem, PurchaseItemDTO>() // Model -> DTO
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Medicine.Name));
        }
    }
}
