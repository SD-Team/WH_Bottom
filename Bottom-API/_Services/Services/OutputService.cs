using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class OutputService : IOutputService
    {
        private readonly IPackingListRepository _repoPackingList;
        private readonly IQRCodeMainRepository _repoQRCodeMain;
        private readonly IQRCodeDetailRepository _repoQRCodeDetail;
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public OutputService(
            IPackingListRepository repoPackingList,
            IQRCodeMainRepository repoQRCodeMain,
            IQRCodeDetailRepository repoQRCodeDetail,
            ITransactionMainRepo repoTransactionMain,
            ITransactionDetailRepo repoTransactionDetail,
            IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoQRCodeMain = repoQRCodeMain;
            _repoQRCodeDetail = repoQRCodeDetail;
            _repoPackingList = repoPackingList;
            _repoTransactionMain = repoTransactionMain;
            _repoTransactionDetail = repoTransactionDetail;
        }
        public async Task<Output_Dto> GetByQrCodeId(string qrCodeId)
        {
            Output_Dto model = new Output_Dto();
            var qrCodeModel = await _repoQRCodeMain.FindAll(x => x.QRCode_ID.Trim() == qrCodeId.ToString().Trim()).OrderByDescending(x => x.QRCode_Version).FirstOrDefaultAsync();
            if (qrCodeModel != null)
            {
                var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                var transctionModel = await _repoTransactionMain.FindAll(x => x.QRCode_ID.Trim() == qrCodeId.ToString().Trim() && x.Transac_Type == "I").FirstOrDefaultAsync();
                model.OutputSheetNo = "OB" + DateTime.Now.ToString("yyyyMMdd") + "001";
                model.QrCodeId = qrCodeModel.QRCode_ID.Trim();
                model.PlanNo = packingListModel.MO_No.Trim();
                model.SupplierName = packingListModel.Supplier_Name.Trim();
                model.SupplierNo = packingListModel.Supplier_ID.Trim();
                model.Batch = packingListModel.MO_Seq;
                model.MatId = packingListModel.Material_ID.Trim();
                model.MatName = packingListModel.Material_Name.Trim();
                model.WH = "";
                model.Building = "";
                model.Area = "";
                model.RackLocation = transctionModel.Rack_Location;
                model.InStockQty = _repoTransactionDetail.GetQtyByTransacNo(transctionModel.Transac_No);
                model.TransOutQty = 0;
                model.RemainingQty = _repoTransactionDetail.GetQtyByTransacNo(transctionModel.Transac_No);
                model.OutputDetail = await _repoTransactionDetail.FindAll(x => x.Transac_No == transctionModel.Transac_No).ProjectTo<TransferLocationDetail_Dto>(_configMapper).ToListAsync();
                model.RackLocation = "";
                // model.InStockQty = _repoTransactionDetail.GetTransQtyByTransacNo(transctionModel.Transac_No);
                // model.TransOutQty = 0;
                // model.RemainingQty = _repoTransactionDetail.GetTransQtyByTransacNo(transctionModel.Transac_No);
            }

            return model;
        }

        public Task<List<OutputDetail_Dto>> GetDetailOutput(string transacNo)
        {
            throw new NotImplementedException();
        }
    }
}