using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface IQRCodeMainRepository : IBottomRepository<WMSB_QRCode_Main>
    {
        Task<WMSB_QRCode_Main> GetByQRCodeID(object qrCodeID);
        Task<List<WMSB_QRCode_Main>> CheckQrCodeID(object qrCodeID);

        WMSB_QRCode_Main GetByQRCodeIDAndVersion(object qrCodeID, object version);
    }
}