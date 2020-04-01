using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class MaterialMissingRepository : BottomRepository<WMSB_Material_Missing>, IMaterialMissingRepository
    {
        private readonly DataContext _context;
        public MaterialMissingRepository(DataContext context) : base(context) {
            _context = context;
        }
    }
}