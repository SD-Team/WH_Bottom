using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories
{
    public class TransactionDetailRepo : BottomRepository<WMSB_Transaction_Detail>, ITransactionDetailRepo
    {
        private readonly DataContext _context;
        public TransactionDetailRepo(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}