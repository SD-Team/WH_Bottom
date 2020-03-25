using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class QRCodeDetailRepository : BottomRepository<WMSB_QRCode_Detail>, IQRCodeDetailRepository
    {
        private readonly DataContext _context;
        public QRCodeDetailRepository(DataContext context) : base(context) {
            _context = context;
        }
    }
}