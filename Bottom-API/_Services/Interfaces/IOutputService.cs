using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;

namespace Bottom_API._Services.Interfaces
{
    public interface IOutputService
    {
        Task<Output_Dto> GetByQrCodeId(string qrCodeId);
        Task<bool> SaveOutput(OutputParam outputParam);
        Task<OutputDetail_Dto> GetDetailOutput(string transacNo);
    }
}