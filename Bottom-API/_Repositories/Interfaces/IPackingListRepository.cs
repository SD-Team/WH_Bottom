using System.Threading.Tasks;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface IPackingListRepository : IBottomRepository<WMSB_Packing_List>
    {
        Task<WMSB_Packing_List> GetByReceiveNo(object receiveNo);
    }
}