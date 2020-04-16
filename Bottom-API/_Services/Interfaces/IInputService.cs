using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;

namespace Bottom_API._Services.Interfaces
{
    public interface IInputService 
    {
        Task<Transaction_Dto> GetByQRCodeID(object qrCodeID);
        Task<Transaction_Detail_Dto> GetDetailByQRCodeID(object qrCodeID);
        Task<bool> CreateInput(Transaction_Detail_Dto model);
        Task<bool> SubmitInput(List<string> lists);
        Task<MissingPrint_Dto> GetMaterialPrint(string missingNo);
        Task<PagedList<Transaction_Main_Dto>> FilterQrCodeAgain(PaginationParams param, FilterQrCodeAgainParam filterParam);
        Task<string> FindMaterialName(string materialID);
        
    }
}