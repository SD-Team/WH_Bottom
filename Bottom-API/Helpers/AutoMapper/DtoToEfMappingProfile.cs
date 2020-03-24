using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
<<<<<<< HEAD
            CreateMap<HPVendorU01Dto, HP_Vendor_u01>();
            CreateMap<WMSB_QRCode_MainDto, WMSB_QRCode_Main>();
            CreateMap<WMSB_Packing_ListDto, WMSB_Packing_List>();
=======
            CreateMap<CodeID_Detail_Dto, WMS_Code>();
            CreateMap<RackLocation_Main_Dto, WMSB_RackLocation_Main>();
>>>>>>> 0033a102b758854207a06eb4a4c64f8f840c216f
        }
    }
}