using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface IOutputService
    {
        Task<Output_Dto> GetByQrCodeId(string qrCodeId);
        Task<List<OutputDetail_Dto>> GetDetailOutput(string transacNo);
    }
}