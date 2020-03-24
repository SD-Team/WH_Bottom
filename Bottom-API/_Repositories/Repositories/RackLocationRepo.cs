using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class RackLocationRepo : BottomRepository<WMSB_RackLocation_Main>, IRackLocationRepo
    {
        private readonly DataContext _context;
        public RackLocationRepo(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}