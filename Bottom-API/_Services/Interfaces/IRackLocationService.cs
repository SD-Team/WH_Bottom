using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;
namespace Bottom_API._Services.Interfaces
{
    public interface IRackLocationService : IBottomService<RackLocation_Main_Dto>
    {
        Task<List<RackLocation_Main_Dto>> Filter(FilterRackLocationParam filterParam);
    }
}