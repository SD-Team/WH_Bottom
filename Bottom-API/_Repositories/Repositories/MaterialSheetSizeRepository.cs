using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class MaterialSheetSizeRepository : BottomRepository<WMSB_Material_Sheet_Size>, IMaterialSheetSizeRepository
    {
        public MaterialSheetSizeRepository(DataContext context) : base(context)
        {
        }
    }
}