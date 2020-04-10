using System.Threading.Tasks;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface ITransactionDetailRepo : IBottomRepository<WMSB_Transaction_Detail>
    {
        decimal? GetQtyByTransacNo(string transacNo);
    }
}