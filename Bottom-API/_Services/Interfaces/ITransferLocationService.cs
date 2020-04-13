using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;

namespace Bottom_API._Services.Interfaces
{
    public interface ITransferLocationService
    {
        Task<TransferLocation_Dto> GetByQrCodeId(object qrCodeId);
        Task<bool> SubmitTransfer(List<TransferLocation_Dto> lists);
        Task<PagedList<TransferLocation_Dto>> Search(TransferLocationParam transferLocationParam, PaginationParams paginationParams);
        Task<List<TransferLocationDetail_Dto>> GetDetailTransaction(string transacNo);
    }
}