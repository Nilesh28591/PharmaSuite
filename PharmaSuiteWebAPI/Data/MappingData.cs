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
            CreateMap<Medicine_Management, Medicine_Dto>().ReverseMap();

        }
    }
}
