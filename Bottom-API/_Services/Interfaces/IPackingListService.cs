using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IPackingListService : IBottomService<Packing_List_Dto>
    {
        Task<PagedList<Packing_List_Dto>> SearchViewModel(PaginationParams param,PackingListSearchViewModel model);
        Task<Packing_List_Dto> FindBySupplier(string supplier_ID);
    }
}