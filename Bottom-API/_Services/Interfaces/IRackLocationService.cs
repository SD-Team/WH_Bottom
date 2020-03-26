using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
namespace Bottom_API._Services.Interfaces
{
    public interface IRackLocationService : IBottomService<RackLocation_Main_Dto>
    {
        Task<PagedList<RackLocation_Main_Dto>> Filter(PaginationParams param, FilterRackLocationParam filterParam);
    }
}