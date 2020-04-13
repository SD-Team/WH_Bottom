using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface ITransferLocationService
    {
        Task<TransferLocation_Dto> GetByQrCodeId(object qrCodeId);
        Task<bool> SubmitTransfer(List<TransferLocation_Dto> lists);
        Task<List<TransferLocation_Dto>> Search(string keyword, string fromDate, string toDate);
    }
}