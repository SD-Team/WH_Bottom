using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
            CreateMap<WMS_Code, CodeID_Detail_Dto>();
            CreateMap<WMSB_RackLocation_Main, RackLocation_Main_Dto>();
        }
        
    }
}