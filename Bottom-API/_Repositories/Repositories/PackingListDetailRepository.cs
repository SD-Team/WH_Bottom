using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class PackingListDetailRepository : BottomRepository<WMSB_PackingList_Detail>, IPackingListDetailRepository
    {
        private readonly DataContext _context;
        public PackingListDetailRepository(DataContext context) : base(context) {
            _context = context;
        }
    }
}