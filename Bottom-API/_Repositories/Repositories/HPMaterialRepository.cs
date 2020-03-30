using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class HPMaterialRepository: HPRepository<HP_Material_j13>, IHPMaterialRepository
    {
        private readonly HPDataContext _context;
        public HPMaterialRepository(HPDataContext context) : base(context) {
            _context = context;
        }
    }
}