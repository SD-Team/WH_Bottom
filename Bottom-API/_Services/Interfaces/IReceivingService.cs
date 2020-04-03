using System.Threading.Tasks;
using Bottom_API.DTO;
using Bottom_API.Helpers;

namespace Bottom_API._Services.Interfaces
{
    public interface IReceivingService : IBottomService<Receiving_Dto>
    {
        Task<PagedList<Receiving_Dto>> Filter(PaginationParams param);
    }
}