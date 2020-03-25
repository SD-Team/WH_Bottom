using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
            CreateMap<WMSB_QRCode_Main, QRCode_Main_Dto>();
            CreateMap<WMSB_Packing_List, Packing_List_Dto>();
            CreateMap<WMS_Code, CodeID_Detail_Dto>();
            CreateMap<WMSB_RackLocation_Main, RackLocation_Main_Dto>();
            CreateMap<WMSB_PackingList_Detail, Packing_List_Detail_Dto>();
            CreateMap<WMSB_QRCode_Detail, QRCode_Detail_Dto>();
        }
    }
}