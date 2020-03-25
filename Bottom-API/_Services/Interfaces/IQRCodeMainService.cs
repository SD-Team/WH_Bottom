using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface IQRCodeMainService : IBottomService<QRCode_Main_Dto>
    {
        Task<bool> AddListQRCode(List<string> listReceiveNo);
    }
}