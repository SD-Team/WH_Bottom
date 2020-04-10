using System.Threading.Tasks;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface ITransactionMainRepo : IBottomRepository<WMSB_Transaction_Main>
    {
        
        Task<WMSB_Transaction_Main> GetByInputNo(object inputNo);

    }
}