using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            CreateMap<CodeID_Detail_Dto, WMS_Code>();
            CreateMap<RackLocation_Main_Dto, WMSB_RackLocation_Main>();
        }
    }
}