using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface ITransferLocationService : IBottomService<Transaction_Dto>
    {
        Task<Transaction_Dto> GetByQRCodeID(object qrCodeID);
    }
}