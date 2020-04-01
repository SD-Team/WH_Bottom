using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IMaterialPurchaseService : IBottomService<Material_Dto>
    {
        Task<PagedList<Material_Dto>> SearchByModel(PaginationParams param, MaterialSearchViewModel model);
    }
}