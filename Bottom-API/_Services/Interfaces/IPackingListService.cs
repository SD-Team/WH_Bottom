using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.Models;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IPackingListService : IBottomService<Packing_List_Dto>
    {
        Task<PagedList<WMSB_Packing_List>> SearchViewModel(PaginationParams param,FilterPackingListParam filterParam);
        Task<List<WMSB_Packing_List>> SearchNotPagination(FilterPackingListParam filterParam);
        Task<Packing_List_Dto> FindBySupplier(string supplier_ID);
        Task<List<SupplierModel>> SupplierList();
        
    }
}