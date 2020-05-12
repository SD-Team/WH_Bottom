using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Models;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IPackingListDetailService : IBottomService<Packing_List_Detail_Dto>
    {
        Task<object> FindByQrCodeID(QrCodeIDVersion data);
        Task<object> FindByQrCodeIDAgain(QrCodeIDVersion data);
        Task<List<object>> PrintByQRCodeIDList(List<QrCodeIDVersion> data);
        Task<List<object>> PrintByQRCodeIDListAgain(List<QrCodeIDVersion> data);
        Task<List<WMSB_PackingList_Detail>> TestPackingList (QrCodeIDVersion data);
    }
}