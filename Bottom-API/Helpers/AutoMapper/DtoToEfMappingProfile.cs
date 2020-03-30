using AutoMapper;
using Bottom_API.DTO;
using Bottom_API.Models;

namespace Bottom_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            CreateMap<QRCode_Main_Dto, WMSB_QRCode_Main>();
            CreateMap<Packing_List_Dto, WMSB_Packing_List>();
            CreateMap<Packing_List_Detail_Dto, WMSB_PackingList_Detail>();
            CreateMap<CodeID_Detail_Dto, WMS_Code>();
            CreateMap<RackLocation_Main_Dto, WMSB_RackLocation_Main>();
            CreateMap<QRCode_Detail_Dto, WMSB_QRCode_Detail>();
            CreateMap<HP_Material_Dto, HP_Material_j13>();
            CreateMap<HP_Style_Dto, HP_Style_j08>();
            CreateMap<HP_Vendor_Dto, HP_Vendor_u01>();
        }
    }
}