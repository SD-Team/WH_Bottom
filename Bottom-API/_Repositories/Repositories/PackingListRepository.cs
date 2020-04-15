using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bottom_API._Repositories.Repositories
{
    public class PackingListRepository : BottomRepository<WMSB_Packing_List>, IPackingListRepository
    {
        private readonly DataContext _context;
        public PackingListRepository(DataContext context) : base(context) {
            _context = context;
        }

        public async Task<WMSB_Packing_List> GetByReceiveNo(object receiveNo)
        {
            var model = await _context.WMSB_Packing_List.FirstOrDefaultAsync(x => x.Receive_No.Trim() == receiveNo.ToString().Trim());
            return model;
        }

        public WMSB_Packing_List GetPackingList(string Purchase_No, string MO_No, string MO_Seq, string Material_ID)
        {
            var model = _context.WMSB_Packing_List.FirstOrDefault(x => x.Purchase_No == Purchase_No && x.MO_No == MO_No && x.MO_Seq == MO_Seq && x.Material_ID == Material_ID);
            return model;
        }
    }
}