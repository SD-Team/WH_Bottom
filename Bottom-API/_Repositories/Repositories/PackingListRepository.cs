using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class PackingListRepository : WMSRepository<WMSB_Packing_List>, IPackingListRepository
    {
        private readonly WMS_DataContext _context;
        public PackingListRepository(WMS_DataContext context) : base(context) {
            _context = context;
        }
    }
}