using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class HPStyleRepository : HPRepository<HP_Style_j08>, IHPStyleRepository
    {
        private readonly HPDataContext _context;
        public HPStyleRepository(HPDataContext context) : base(context) {
            _context = context;
        }
    }
}