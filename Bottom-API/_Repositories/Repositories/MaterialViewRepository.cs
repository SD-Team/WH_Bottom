using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class MaterialViewRepository : BottomRepository<VM_WMSB_Material_Purchase>, IMaterialViewRepository
    {
        private readonly DataContext _context;
        public MaterialViewRepository(DataContext context) : base(context) {
            _context = context;
        }
    }
}