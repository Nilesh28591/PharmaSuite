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
            // From Entity to DTO (for Get/Read)
            CreateMap<Purchase, PurchaseDTO>() // Model -> DTO
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Supplier.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Supplier.Email))
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<PurchaseItem, PurchaseItemDTO>() // Model -> DTO
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Medicine.Name));
        }
    }
}
