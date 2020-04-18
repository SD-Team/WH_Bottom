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
using Bottom_API.Helpers;
using Bottom_API.Models;

namespace Bottom_API._Services.Services
{
    public class OutputService : IOutputService
    {
        private readonly IPackingListRepository _repoPackingList;
        private readonly IQRCodeMainRepository _repoQRCodeMain;
        private readonly IQRCodeDetailRepository _repoQRCodeDetail;
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly IMaterialSheetSizeRepository _repoMaterialSheetSize;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public OutputService(
            IPackingListRepository repoPackingList,
            IQRCodeMainRepository repoQRCodeMain,
            IQRCodeDetailRepository repoQRCodeDetail,
            ITransactionMainRepo repoTransactionMain,
            ITransactionDetailRepo repoTransactionDetail,
            IMaterialSheetSizeRepository repoMaterialSheetSize,
            IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoQRCodeMain = repoQRCodeMain;
            _repoQRCodeDetail = repoQRCodeDetail;
            _repoPackingList = repoPackingList;
            _repoTransactionMain = repoTransactionMain;
            _repoTransactionDetail = repoTransactionDetail;
            _repoMaterialSheetSize = repoMaterialSheetSize;
        }

        public async Task<Output_Dto> GetByQrCodeId(string qrCodeId)
        {
            var listMaterialSheetSize = await _repoMaterialSheetSize.FindAll(x => x.Sheet_No.Trim() == qrCodeId.Trim()).ProjectTo<Material_Sheet_Size_Dto>(_configMapper).ToListAsync();

            List<OutputMain_Dto> listOuput = new List<OutputMain_Dto>();
            var materialSheetSize = await _repoMaterialSheetSize.FindAll(x => x.Sheet_No.Trim() == qrCodeId.Trim()).FirstOrDefaultAsync();
            if (materialSheetSize != null)
            {
                var transactionModel = await _repoTransactionMain.FindAll(x => x.MO_No.Trim() == materialSheetSize.Manno.Trim() && x.MO_Seq.Trim() == materialSheetSize.Batch.Trim() && x.Material_ID == materialSheetSize.Material_ID && x.Can_Move == "Y" && x.Transac_Type != "O").ToListAsync();

                foreach (var item in transactionModel)
                {
                    OutputMain_Dto output = new OutputMain_Dto();
                    output.Id = item.ID;
                    output.TransacNo = item.Transac_No;
                    output.QrCodeId = item.QRCode_ID.Trim();
                    output.PlanNo = item.MO_No.Trim();
                    output.Batch = item.MO_Seq;
                    output.MatId = item.Material_ID.Trim();
                    output.MatName = item.Material_Name.Trim();
                    output.Building = "";
                    output.Area = "";
                    output.RackLocation = item.Rack_Location;
                    output.InStockQty = _repoTransactionDetail.GetQtyByTransacNo(item.Transac_No);
                    output.TransOutQty = 0;
                    output.RemainingQty = _repoTransactionDetail.GetQtyByTransacNo(item.Transac_No);

                    var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(item.QRCode_ID);
                    var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                    // output.SupplierName = packingListModel.Supplier_Name.Trim();
                    // output.SupplierNo = packingListModel.Supplier_ID.Trim();

                    listOuput.Add(output);
                }
            }

            Output_Dto result = new Output_Dto();
            result.Outputs = listOuput.OrderBy(x => x.InStockQty).ToList();
            result.MaterialSheetSizes = listMaterialSheetSize;
            return result;
        }

        public async Task<bool> SaveOutput(OutputParam outputParam)
        {
            // Tìm ra TransactionMain theo id
            var transactionMain = _repoTransactionMain.FindSingle(x => x.ID == outputParam.output.Id);

            WMSB_Transaction_Main model = new WMSB_Transaction_Main();
            model = transactionMain;
            model.ID = 0;
            model.Transac_Type = "O";
            model.Transac_No = outputParam.output.TransacNo;
            model.Transac_Sheet_No = "";
            model.Transac_Time = DateTime.Now;
            model.Updated_Time = DateTime.Now;
            model.Updated_By = "Emma";
            _repoTransactionMain.Add(model);

            foreach (var item in outputParam.transactionDetail)
            {
                var itemModel = _mapper.Map<WMSB_Transaction_Detail>(item);
                _repoTransactionDetail.Add(itemModel);
            }

            // Nếu output ra chưa hết
            // if (outputParam.output.RemainingQty > 0)
            // {
            //     model.Transac_Type = "R";
            //     model.Transac_No = "R" + transactionMain.Transac_No;
            // }

            return await _repoTransactionMain.SaveAll();
        }
    }
}