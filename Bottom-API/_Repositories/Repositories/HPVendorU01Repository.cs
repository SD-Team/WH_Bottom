
using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;


namespace Bottom_API._Repositories.Repositories
{
    public class HPVendorU01Repository: HPRepository<HP_Vendor_u01>, IHPVendorU01Repository
    {
        private readonly HP_DataContext _context;
        public HPVendorU01Repository(HP_DataContext context) : base(context) {
            _context = context;
        }

    }
}