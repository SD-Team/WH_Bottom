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
            CreateMap<QRCode_Main_Dto, WMSB_QRCode_Main>();
            CreateMap<Packing_List_Dto, WMSB_Packing_List>();

            CreateMap<CodeID_Detail_Dto, WMS_Code>();
            CreateMap<RackLocation_Main_Dto, WMSB_RackLocation_Main>();

        }
    }
}