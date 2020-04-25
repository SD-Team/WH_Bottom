using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Repositories.Repositories
{
    public class QRCodeDetailRepository : BottomRepository<WMSB_QRCode_Detail>, IQRCodeDetailRepository
    {
        private readonly DataContext _context;
        public QRCodeDetailRepository(DataContext context) : base(context) {
            _context = context;
        }

        public async Task<List<WMSB_QRCode_Detail>> GetByQRCodeIDAndVersion(object qrCodeID, int version)
        {
            var lists = await _context.WMSB_QRCode_Detail.Where(x => x.QRCode_ID == qrCodeID.ToString() && x.QRCode_Version == version).ToListAsync();
            return lists;
        }
    }
}