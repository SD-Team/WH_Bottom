using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface ITransferLocationService
    {
        Task<TransferLocation_Dto> GetByQrCodeId(object qrCodeId);
    }
}