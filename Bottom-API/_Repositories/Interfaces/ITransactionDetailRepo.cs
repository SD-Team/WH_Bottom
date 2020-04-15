using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface ITransactionDetailRepo : IBottomRepository<WMSB_Transaction_Detail>
    {
        decimal? GetQtyByTransacNo(string transacNo);

        List<WMSB_Transaction_Detail> GetListTransDetailByTransacNo(string transacNo);
    }
}