using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface IInputService : IBottomService<Transaction_Dto>
    {
        Task<Transaction_Dto> GetByQRCodeID(object qrCodeID);
    }
}