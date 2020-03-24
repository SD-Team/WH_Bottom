using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
<<<<<<< HEAD
            CreateMap<HP_Vendor_u01, HPVendorU01Dto>();
            CreateMap<WMSB_QRCode_Main, WMSB_QRCode_MainDto>();
            CreateMap<WMSB_Packing_List, WMSB_Packing_ListDto>();
=======
            CreateMap<WMS_Code, CodeID_Detail_Dto>();
            CreateMap<WMSB_RackLocation_Main, RackLocation_Main_Dto>();
>>>>>>> 0033a102b758854207a06eb4a4c64f8f840c216f
        }
    }
}