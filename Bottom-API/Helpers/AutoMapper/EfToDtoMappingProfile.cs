using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
            CreateMap<HP_Vendor_u01, HPVendorU01Dto>();
            CreateMap<WMSB_QRCode_Main, WMSB_QRCode_MainDto>();
            CreateMap<WMSB_Packing_List, WMSB_Packing_ListDto>();
        }
    }
}