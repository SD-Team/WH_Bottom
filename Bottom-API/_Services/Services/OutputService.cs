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
            DateTime timeNow = DateTime.Now;
            Random ran = new Random();
            int num = ran.Next(100, 999);

            // Tìm ra TransactionMain theo id
            var transactionMain = _repoTransactionMain.FindSingle(x => x.ID == outputParam.output.Id);
            transactionMain.Can_Move = "N";
            _repoTransactionMain.Update(transactionMain);

            // thêm type O
            WMSB_Transaction_Main modelTypeO = new WMSB_Transaction_Main();
            modelTypeO.Transac_Type = "O";
            modelTypeO.Can_Move = "N";
            modelTypeO.Transac_No = outputParam.output.TransacNo;
            modelTypeO.Transac_Sheet_No = outputParam.output.RemainingQty > 0 ? "OB" + DateTime.Now.ToString("yyyyMMdd") + num.ToString() : "";
            modelTypeO.Transacted_Qty = outputParam.output.TransOutQty;
            modelTypeO.Pickup_No = outputParam.output.PickupNo;
            modelTypeO.Transac_Time = timeNow;
            modelTypeO.Updated_Time = timeNow;
            modelTypeO.Updated_By = "Emma";
            modelTypeO.Missing_No = transactionMain.Missing_No;
            modelTypeO.Material_ID = transactionMain.Material_ID;
            modelTypeO.Material_Name = transactionMain.Material_Name;
            modelTypeO.Purchase_No = transactionMain.Purchase_No;
            modelTypeO.Rack_Location = transactionMain.Rack_Location;
            modelTypeO.Purchase_Qty = transactionMain.Purchase_Qty;
            modelTypeO.QRCode_Version = transactionMain.QRCode_Version;
            modelTypeO.QRCode_ID = transactionMain.QRCode_ID;
            modelTypeO.MO_No = transactionMain.MO_No;
            modelTypeO.MO_Seq = transactionMain.MO_Seq;
            _repoTransactionMain.Add(modelTypeO);

            foreach (var item in outputParam.transactionDetail)
            {
                item.ID = 0;
                item.Transac_No = outputParam.output.TransacNo;
                item.Updated_By = "Emma";
                item.Updated_Time = timeNow;
                var itemModel = _mapper.Map<WMSB_Transaction_Detail>(item);
                _repoTransactionDetail.Add(itemModel);
            }

            // Nếu output ra chưa hết
            if (outputParam.output.RemainingQty > 0)
            {
                //  thêm type R
                // nếu type là R thì update lại
                if (transactionMain.Transac_Type.Trim() == "R")
                {
                    transactionMain.Updated_Time = timeNow;
                    transactionMain.Updated_By = "Emma";
                    transactionMain.Transacted_Qty = outputParam.output.TransOutQty;
                    _repoTransactionMain.Update(transactionMain);

                    var transactionDetail = await _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transactionMain.Transac_No.Trim()).ToListAsync();
                    foreach (var item in transactionDetail)
                    {
                        item.Trans_Qty = outputParam.transactionDetail.Where(x => x.Tool_Size == item.Tool_Size && x.Order_Size == item.Order_Size && x.Model_Size == item.Model_Size).FirstOrDefault().Trans_Qty;
                        item.Qty = outputParam.transactionDetail.Where(x => x.Tool_Size == item.Tool_Size && x.Order_Size == item.Order_Size && x.Model_Size == item.Model_Size).FirstOrDefault().Qty;
                        item.Instock_Qty = outputParam.transactionDetail.Where(x => x.Tool_Size == item.Tool_Size && x.Order_Size == item.Order_Size && x.Model_Size == item.Model_Size).FirstOrDefault().Instock_Qty;
                        item.Updated_By = "Emma";
                        item.Updated_Time = timeNow;
                        _repoTransactionDetail.Update(item);
                    }
                }
                // ngược lại thì thêm mới type R
                else
                {
                    WMSB_Transaction_Main modelTypeR = new WMSB_Transaction_Main();
                    modelTypeR.Transac_Type = "R";
                    modelTypeR.Transac_No = "R" + transactionMain.Transac_No;
                    modelTypeR.Transac_Sheet_No = "R" + transactionMain.Transac_No;
                    modelTypeR.Transacted_Qty = outputParam.output.TransOutQty;
                    modelTypeR.Updated_By = "Emma";
                    modelTypeR.Updated_Time = timeNow;
                    modelTypeR.Missing_No = transactionMain.Missing_No;
                    modelTypeR.Material_ID = transactionMain.Material_ID;
                    modelTypeR.Material_Name = transactionMain.Material_Name;
                    modelTypeR.Purchase_No = transactionMain.Purchase_No;
                    modelTypeR.Rack_Location = transactionMain.Rack_Location;
                    modelTypeR.Purchase_Qty = transactionMain.Purchase_Qty;
                    modelTypeR.QRCode_Version = transactionMain.QRCode_Version;
                    modelTypeR.QRCode_ID = transactionMain.QRCode_ID;
                    modelTypeR.MO_No = transactionMain.MO_No;
                    modelTypeR.MO_Seq = transactionMain.MO_Seq;
                    modelTypeR.Can_Move = "Y";
                    modelTypeR.Transac_Time = transactionMain.Transac_Time;
                    _repoTransactionMain.Add(modelTypeR);
                    // thêm transactiondetail của type R
                    foreach (var itemTypeR in outputParam.transactionDetail)
                    {
                        itemTypeR.ID = 0;
                        itemTypeR.Transac_No = modelTypeR.Transac_No;
                        itemTypeR.Updated_By = "Emma";
                        itemTypeR.Updated_Time = timeNow;
                        var itemModel = _mapper.Map<WMSB_Transaction_Detail>(itemTypeR);
                        _repoTransactionDetail.Add(itemModel);
                    }
                }

                // thêm qrcode mới
                var qrCodeMain = await _repoQRCodeMain.FindAll(x => x.QRCode_ID.Trim() == outputParam.output.QrCodeId.Trim()).OrderByDescending(x => x.QRCode_Version).FirstOrDefaultAsync();
                WMSB_QRCode_Main modelQrCodeMain = new WMSB_QRCode_Main();
                modelQrCodeMain.QRCode_ID = qrCodeMain.QRCode_ID;
                modelQrCodeMain.QRCode_Type = qrCodeMain.QRCode_Type;
                modelQrCodeMain.Receive_No = qrCodeMain.Receive_No;
                modelQrCodeMain.Valid_Status = qrCodeMain.Valid_Status;
                modelQrCodeMain.Invalid_Date = qrCodeMain.Invalid_Date;
                modelQrCodeMain.QRCode_Version = qrCodeMain.QRCode_Version + 1;
                modelQrCodeMain.Updated_Time = timeNow;
                modelQrCodeMain.Updated_By = "Emma";
                _repoQRCodeMain.Add(modelQrCodeMain);
                // thêm qrcodedetail của qrcode mới
                var qrCodeDetails = await _repoQRCodeDetail.FindAll(x => x.QRCode_ID.Trim() == qrCodeMain.QRCode_ID.Trim() && x.QRCode_Version == qrCodeMain.QRCode_Version).ToListAsync();
                foreach (var itemQrCodeDetail in qrCodeDetails)
                {
                    itemQrCodeDetail.QID = 0;
                    itemQrCodeDetail.Updated_By = "Emma";
                    itemQrCodeDetail.Updated_Time = timeNow;
                    itemQrCodeDetail.QRCode_Version = modelQrCodeMain.QRCode_Version;
                    itemQrCodeDetail.Qty = outputParam.transactionDetail.Where(x => x.Tool_Size == itemQrCodeDetail.Tool_Size && x.Order_Size == itemQrCodeDetail.Order_Size && x.Model_Size == itemQrCodeDetail.Model_Size).FirstOrDefault().Instock_Qty;
                    _repoQRCodeDetail.Add(itemQrCodeDetail);
                }
            }

            return await _repoTransactionMain.SaveAll();
        }
    }
}