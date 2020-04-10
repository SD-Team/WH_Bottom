using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Repositories.Repositories
{
    public class TransactionMainRepo : BottomRepository<WMSB_Transaction_Main>, ITransactionMainRepo
    {
        private readonly DataContext _context;
        public TransactionMainRepo(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}