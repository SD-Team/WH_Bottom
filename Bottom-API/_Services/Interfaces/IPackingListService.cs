using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.Models;

namespace Bottom_API._Services.Interfaces
{
    public interface IPackingListService : IBottomService<Packing_List_Dto>
    {
        Task<PagedList<Packing_List_Dto>> SearchViewModel(PaginationParams param,FilterPackingListParam filterParam);
        Task<string> FindBySupplier(string supplier_ID);
    }
}