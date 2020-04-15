using System.Linq;
using System.Threading.Tasks;
using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Repositories.Repositories
{
    public class QRCodeMainRepository : BottomRepository<WMSB_QRCode_Main>, IQRCodeMainRepository
    {
        private readonly DataContext _context;
        public QRCodeMainRepository(DataContext context) : base(context) {
            _context = context;
        }

        public async Task<WMSB_QRCode_Main> GetByQRCodeID(object qrCodeID)
        {
            var model = await _context.WMSB_QRCode_Main.FirstOrDefaultAsync(x => x.QRCode_ID.Trim() == qrCodeID.ToString().Trim());
            return model;
        }
         public WMSB_QRCode_Main GetByQRCodeIDAndVersion(object qrCodeID, object version)
        {
            var model = _context.WMSB_QRCode_Main.FirstOrDefault(x => x.QRCode_ID.Trim() == qrCodeID.ToString().Trim() && x.QRCode_Version.ToString() == version.ToString());
            return model;
        }
    }
}