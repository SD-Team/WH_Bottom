using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class QRCodeMainRepository : BottomRepository<WMSB_QRCode_Main>, IQRCodeMainRepository
    {
        private readonly DataContext _context;
        public QRCodeMainRepository(DataContext context) : base(context) {
            _context = context;
        }
    }
}