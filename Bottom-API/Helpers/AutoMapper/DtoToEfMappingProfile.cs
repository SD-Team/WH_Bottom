using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            CreateMap<HPVendorU01Dto, HP_Vendor_u01>();
            CreateMap<WMSB_QRCode_MainDto, WMSB_QRCode_Main>();
            CreateMap<WMSB_Packing_ListDto, WMSB_Packing_List>();
        }
    }
}