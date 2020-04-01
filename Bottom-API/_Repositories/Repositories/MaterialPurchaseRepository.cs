using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class MaterialPurchaseRepository : BottomRepository<WMSB_Material_Purchase>, IMaterialPurchaseRepository
    {
        private readonly DataContext _context;
        public MaterialPurchaseRepository(DataContext context) : base(context) {
            _context = context;
        }
    }
}