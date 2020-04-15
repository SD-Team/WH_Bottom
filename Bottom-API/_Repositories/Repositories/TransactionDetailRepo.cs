using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<WMSB_Transaction_Detail> GetListTransDetailByTransacNo(string transacNo)
        {
            var lists = _context.WMSB_Transaction_Detail.Where(x => x.Transac_No.Trim() == transacNo).ToList();
            return lists;
        }

        public decimal? GetQtyByTransacNo(string transacNo)
        {
            var data = _context.WMSB_Transaction_Detail.Where(x => x.Transac_No.Trim() == transacNo).Sum(x => x.Instock_Qty);
            return data;
        }

        public decimal? GetTransQtyByTransacNo(string transacNo)
        {
            var data = _context.WMSB_Transaction_Detail.Where(x => x.Transac_No.Trim() == transacNo).Sum(x => x.Trans_Qty);
            return data;
        }

        public decimal? GetUntransacQtyByTransacNo(string transacNo)
        {
            var data = _context.WMSB_Transaction_Detail.Where(x => x.Transac_No.Trim() == transacNo).Sum(x => x.Untransac_Qty);
            return data;
        }
    }
}