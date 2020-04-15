using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface IInputService 
    {
        Task<Transaction_Dto> GetByQRCodeID(object qrCodeID);
        Task<Transaction_Detail_Dto> GetDetailByQRCodeID(object qrCodeID);
        Task<bool> CreateInput(Transaction_Detail_Dto model);
        Task<bool> SubmitInput(List<string> lists);
        
    }
}