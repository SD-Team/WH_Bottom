using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;

namespace Bottom_API._Services.Interfaces
{
    public interface IPackingListService : IBottomService<WMSB_Packing_ListDto>
    {
        public Task<PagedList<WMSB_Packing_ListDto>> SearchViewModel(PaginationParams param,PackingListSearchViewModel model);
        public Task<WMSB_Packing_ListDto> FindBySupplier(string supplier_ID);
    }
}