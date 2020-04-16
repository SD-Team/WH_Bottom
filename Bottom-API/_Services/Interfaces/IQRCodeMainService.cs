using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IQRCodeMainService : IBottomService<QRCode_Main_Dto>
    {
        Task<bool> AddListQRCode(List<string> listReceiveNo);
        Task<PagedList<QRCodeMainViewModel>> SearchByPlanNo(PaginationParams param, FilterQrCodeParam filterParam);
        Task<QRCodePrint_Dto> GetQrCodePrint(string qrCodeId, int qrCodeVersion);
    }
}