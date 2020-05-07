using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface IQRCodeDetailRepository : IBottomRepository<WMSB_QRCode_Detail>
    {
        Task<List<WMSB_QRCode_Detail>> GetByQRCodeIDAndVersion(object qrCodeID, int version);
    }
}