using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class QRCodeMainRepository : WMSRepository<WMSB_QRCode_Main>, IQRCodeMainRepository
    {
        private readonly WMS_DataContext _context;
        public QRCodeMainRepository(WMS_DataContext context) : base(context) {
            _context = context;
        }
    }
}