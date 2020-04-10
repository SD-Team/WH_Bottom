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

        public async Task<WMSB_Transaction_Main> GetByInputNo(object inputNo)
        {
            return await _context.WMSB_Transaction_Main.FirstOrDefaultAsync(x => x.Transac_No.Trim() == inputNo.ToString().Trim());
        }

        public Task<WMSB_Transaction_Main> GetByQRCodeD(object qrCodeID)
        {
            throw new NotImplementedException();
        }

        public async Task<WMSB_Transaction_Main> GetByQrCodeId(object qrCodeId)
        {
            var model = await _context.WMSB_Transaction_Main.FirstOrDefaultAsync(x => x.QRCode_ID.Trim() == qrCodeId.ToString().Trim() && x.Can_Move == "Y");
            return model;
        }
        
    }
}