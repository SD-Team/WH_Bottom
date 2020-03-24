using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class PackingListRepository : BottomRepository<WMSB_Packing_List>, IPackingListRepository
    {
        private readonly DataContext _context;
        public PackingListRepository(DataContext context) : base(context) {
            _context = context;
        }
    }
}