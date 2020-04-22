using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IPackingListDetailService : IBottomService<Packing_List_Detail_Dto>
    {
        Task<object> FindByQrCodeID(string qrCodeID);
        Task<List<object>> PrintByQRCodeIDList(List<string> data);
    }
}