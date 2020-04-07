using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IReceivingService : IBottomService<Receiving_Dto>
    {
        Task<PagedList<Receiving_Dto>> Filter(PaginationParams param);
        Task<List<MaterialMainViewModel>> SearchByModel(MaterialSearchViewModel model);
        Task<object> MaterialMerging(MaterialMainViewModel model);
        Task<bool> UpdateMaterial(List<OrderSizeByBatch> data);
    }
}