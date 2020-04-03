using System.Collections.Generic;
using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;
using System.Linq;

namespace Bottom_API._Repositories.Repositories
{
    public class MaterialPurchaseRepository : BottomRepository<WMSB_Material_Purchase>, IMaterialPurchaseRepository
    {
        private readonly DataContext _context;
        public MaterialPurchaseRepository(DataContext context) : base(context) {
            _context = context;
        }

        public List<string> GetPurchase()
        {
            var list = _context.WMSB_Material_Purchase.Select(x => x.Purchase_No).Distinct().ToList();
            return list;
        }
    }
}