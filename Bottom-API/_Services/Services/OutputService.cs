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
        private readonly IRackLocationRepo _repoRackLocation;
        private readonly ICodeIDDetailRepo _repoCode;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public OutputService(
            IPackingListRepository repoPackingList,
            IQRCodeMainRepository repoQRCodeMain,
            IQRCodeDetailRepository repoQRCodeDetail,
            ITransactionMainRepo repoTransactionMain,
            ITransactionDetailRepo repoTransactionDetail,
            IMaterialSheetSizeRepository repoMaterialSheetSize,
            IRackLocationRepo repoRackLocation,
            ICodeIDDetailRepo repoCode,
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
            _repoRackLocation = repoRackLocation;
            _repoCode = repoCode;
        }

        public async Task<Output_Dto> GetByQrCodeId(string qrCodeId)
        {
            // biến qrcodeid là sheet no của bảng materialsheetsize, dựa theo mã đó lấy ra listmaterialsheetsize là danh sánh đơn output ra
            var listMaterialSheetSize = await _repoMaterialSheetSize.FindAll(x => x.Sheet_No.Trim() == qrCodeId.Trim()).ProjectTo<Material_Sheet_Size_Dto>(_configMapper).OrderBy(x => x.Tool_Size).ToListAsync();

            // kiểm tra xem Sheet_No đó đã output bao giờ chưa, nếu có rồi là kiểm tra đơn đó đã output ra hết chưa
            var listTransactionMainTypeOByPickupNo = await _repoTransactionMain.FindAll().Where(x => x.Pickup_No.Trim() == qrCodeId && x.Transac_Type.Trim() == "O").ToListAsync();
            List<WMSB_Transaction_Detail> listTransactionDetailByBistTransactionMainTypeOByPickupNo = new List<WMSB_Transaction_Detail>();
            if (listTransactionMainTypeOByPickupNo.Count > 0)
            {
                foreach (var item in listTransactionMainTypeOByPickupNo)
                {
                    var tmp = await _repoTransactionDetail.FindAll(x => x.Transac_No == item.Transac_No).ToListAsync();
                    listTransactionDetailByBistTransactionMainTypeOByPickupNo.AddRange(tmp);
                }

                var totalTransQtyOutput = listTransactionDetailByBistTransactionMainTypeOByPickupNo.GroupBy(x => new { x.Order_Size })
                    .Select(y => new
                    {
                        OrderSize = y.Key.Order_Size,
                        TotalTransQty = (decimal)y.Sum(z => z.Trans_Qty)
                    }).ToList();

                listMaterialSheetSize = listMaterialSheetSize.Join(totalTransQtyOutput, x => x.Order_Size, y => y.OrderSize, (x, y) => new Material_Sheet_Size_Dto
                {
                    Qty = (decimal)x.Qty - y.TotalTransQty,
                    Factory_ID = x.Factory_ID,
                    Batch = x.Batch,
                    Cur_Ent = x.Cur_Ent,
                    HP_Update_Time = x.HP_Update_Time,
                    HP_User = x.HP_User,
                    Manno = x.Manno,
                    Material_ID = x.Material_ID,
                    Model_Size = x.Model_Size,
                    Order_Size = x.Order_Size,
                    Print_Size = x.Print_Size,
                    Prod_Delivery_Way = x.Prod_Delivery_Way,
                    Sheet_No = x.Sheet_No,
                    Subcont_Material = x.Subcont_Material,
                    Tool_Size = x.Tool_Size,
                    Update_By = x.Update_By,
                    Update_Time = x.Update_Time
                }).ToList();
            }
            // hết kiểm tra xem Sheet_No đó đã output ra bao giờ chưa


            List<OutputMain_Dto> listOuput = new List<OutputMain_Dto>();
            var materialSheetSize = await _repoMaterialSheetSize.FindAll(x => x.Sheet_No.Trim() == qrCodeId.Trim()).FirstOrDefaultAsync();
            if (materialSheetSize != null)
            {
                List<WMSB_Transaction_Main> transactionModel = new List<WMSB_Transaction_Main>();
                // Nếu Prod_Delivery_Way == "A" thì chỉ lấy trong transaction main theo material_id và mo_no
                if (materialSheetSize.Prod_Delivery_Way == "A")
                {
                    transactionModel = await _repoTransactionMain.FindAll(x => x.MO_No.Trim() == materialSheetSize.Manno.Trim() && x.Material_ID == materialSheetSize.Material_ID && x.Can_Move == "Y" && x.Transac_Type != "O").ToListAsync();
                }
                // ngược lại là lấy theeo mo_no, material_id và batch
                else
                {
                    transactionModel = await _repoTransactionMain.FindAll(x => x.MO_No.Trim() == materialSheetSize.Manno.Trim() && x.MO_Seq.Trim() == materialSheetSize.Batch.Trim() && x.Material_ID == materialSheetSize.Material_ID && x.Can_Move == "Y" && x.Transac_Type != "O").ToListAsync();
                }

                foreach (var item in transactionModel)
                {
                    var rackLocation = await _repoRackLocation.FindAll(x => x.Rack_Location.Trim() == item.Rack_Location.Trim()).FirstOrDefaultAsync();
                    // dữ liệu output cần hiển thị: trong bảng tranasaction main, transaction detail ...
                    OutputMain_Dto output = new OutputMain_Dto();
                    output.Id = item.ID;
                    output.TransacNo = item.Transac_No;
                    output.QrCodeId = item.QRCode_ID.Trim();
                    output.QrCodeVersion = item.QRCode_Version;
                    output.PlanNo = item.MO_No.Trim();
                    output.Batch = item.MO_Seq;
                    output.MatId = item.Material_ID.Trim();
                    output.MatName = item.Material_Name.Trim();
                    if (rackLocation != null)
                    {
                        output.Building = _repoCode.FindSingle(x => x.Code_Type == 3 && x.Code_ID == rackLocation.Build_ID).Code_Ename;// building With WMS_Code.Code_TYPE = 3
                        output.Area = _repoCode.FindSingle(x => x.Code_Type == 5 && x.Code_ID == rackLocation.Area_ID).Code_Ename;// building With WMS_Code.Code_TYPE = 5
                        output.WH = _repoCode.FindSingle(x => x.Code_Type == 2 && x.Code_ID == rackLocation.WH_ID).Code_Ename;// building With WMS_Code.Code_TYPE = 2
                    }
                    output.RackLocation = item.Rack_Location;
                    output.InStockQty = _repoTransactionDetail.GetQtyByTransacNo(item.Transac_No);
                    output.TransOutQty = 0;
                    output.RemainingQty = _repoTransactionDetail.GetQtyByTransacNo(item.Transac_No);

                    var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(item.QRCode_ID);
                    var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                    if (packingListModel != null)
                    {
                        output.SupplierName = packingListModel.Supplier_Name.Trim();
                        output.SupplierNo = packingListModel.Supplier_ID.Trim();
                    }

                    listOuput.Add(output);
                }
            }

            // dữ liệu cần lấy ra để hiển thị: listoutputmain là trong bảng transaction main với type bằng I, R, M và listmaterialsheetsize là số lượng cần output ra của đơn
            Output_Dto result = new Output_Dto();
            result.Outputs = listOuput.OrderBy(x => x.InStockQty).ToList();
            result.MaterialSheetSizes = listMaterialSheetSize;
            return result;
        }

        public async Task<OutputDetail_Dto> GetDetailOutput(string transacNo)
        {
            var transactionMain = _repoTransactionMain.FindSingle(x => x.Transac_No.Trim() == transacNo.Trim());
            var transactionDetail = await _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transactionMain.Transac_No.Trim()).ProjectTo<TransferLocationDetail_Dto>(_configMapper).OrderBy(x => x.Tool_Size).ToListAsync();

            // Lấy ra những thuộc tính cần
            OutputDetail_Dto result = new OutputDetail_Dto();
            result.Id = transactionMain.ID;
            result.QrCodeId = transactionMain.QRCode_ID;
            result.PlanNo = transactionMain.MO_No;
            result.MatId = transactionMain.Material_ID;
            result.MatName = transactionMain.Material_Name;
            result.Batch = transactionMain.MO_Seq;
            result.TransactionDetail = transactionDetail;

            return result;
        }
        public async Task<bool> SaveListOutput(List<OutputParam> outputParam)
        {
            DateTime timeNow = DateTime.Now;
            foreach (var itemt in outputParam)
            {
                Random ran = new Random();
                int num = ran.Next(100, 999);

                // Tìm ra TransactionMain theo id
                var transactionMain = _repoTransactionMain.FindSingle(x => x.ID == itemt.output.Id);
                if (transactionMain.Transac_Type != "R")
                {
                    transactionMain.Can_Move = "N"; // nếu type != R update transaction main cũ: Can_Move thành N
                    _repoTransactionMain.Update(transactionMain);
                }
                if (transactionMain.Transac_Type == "R" && itemt.output.RemainingQty == 0)
                {
                    transactionMain.Can_Move = "N"; // nếu type == R và output ra hết update : Can_Move thành N
                    _repoTransactionMain.Update(transactionMain);
                }

                // thêm transaction main type O
                WMSB_Transaction_Main modelTypeO = new WMSB_Transaction_Main();
                modelTypeO.Transac_Type = "O";
                modelTypeO.Can_Move = "N";
                modelTypeO.Transac_No = itemt.output.TransacNo;
                modelTypeO.Transac_Sheet_No = "OB" + DateTime.Now.ToString("yyyyMMdd") + num.ToString();
                modelTypeO.Transacted_Qty = itemt.output.TransOutQty;
                modelTypeO.Pickup_No = itemt.output.PickupNo;
                modelTypeO.Transac_Time = timeNow;
                modelTypeO.Updated_Time = timeNow;
                modelTypeO.Updated_By = "Emma";
                modelTypeO.Missing_No = transactionMain.Missing_No;
                modelTypeO.Material_ID = transactionMain.Material_ID;
                modelTypeO.Material_Name = transactionMain.Material_Name;
                modelTypeO.Purchase_No = transactionMain.Purchase_No;
                modelTypeO.Rack_Location = null;// type O: racklocation rỗng
                modelTypeO.Purchase_Qty = transactionMain.Purchase_Qty;
                modelTypeO.QRCode_Version = transactionMain.QRCode_Version;
                modelTypeO.QRCode_ID = transactionMain.QRCode_ID;
                modelTypeO.MO_No = transactionMain.MO_No;
                modelTypeO.MO_Seq = transactionMain.MO_Seq;
                _repoTransactionMain.Add(modelTypeO);

                // Thêm transaction detail mới theo type = o, dựa vào transaction detail của transaction main cũ
                foreach (var item in itemt.transactionDetail)
                {
                    item.ID = 0;// ID trong db là tự tăng: dựa vào transaction detail cũ nên thêm mới gán id bằng 0, không cần phải new hết thuộc tính của đổi tượng ra
                    item.Transac_No = itemt.output.TransacNo;
                    item.Updated_By = "Emma";
                    item.Updated_Time = timeNow;
                    var itemModel = _mapper.Map<WMSB_Transaction_Detail>(item);
                    _repoTransactionDetail.Add(itemModel);
                }

                // Nếu output ra chưa hết thì thêm transaction main type R, và transaction detail, thêm qrcode mới và update version lên
                if (itemt.output.RemainingQty > 0)
                {
                    //  thêm type R
                    // nếu type là R: thì update lại
                    if (transactionMain.Transac_Type.Trim() == "R")
                    {
                        var tmpQrcodeVersion = transactionMain.QRCode_Version + 1;
                        transactionMain.Updated_Time = timeNow;
                        transactionMain.Updated_By = "Emma";
                        transactionMain.Transacted_Qty = itemt.output.TransOutQty;
                        transactionMain.QRCode_Version = tmpQrcodeVersion;
                        _repoTransactionMain.Update(transactionMain);

                        // update transaction main thì cũng phải update transaction detail
                        var transactionDetail = await _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transactionMain.Transac_No.Trim()).ToListAsync();
                        foreach (var item in transactionDetail)
                        {
                            item.Trans_Qty = 0;// nếu transaction main là Type R là thì transaction detail của nó gán Trans_Qty = 0
                            item.Qty = itemt.transactionDetail.Where(x => x.Tool_Size == item.Tool_Size && x.Order_Size == item.Order_Size && x.Model_Size == item.Model_Size).FirstOrDefault().Qty;
                            item.Instock_Qty = itemt.transactionDetail.Where(x => x.Tool_Size == item.Tool_Size && x.Order_Size == item.Order_Size && x.Model_Size == item.Model_Size).FirstOrDefault().Instock_Qty;
                            item.Updated_By = "Emma";
                            item.Updated_Time = timeNow;
                            _repoTransactionDetail.Update(item);
                        }
                    }
                    // ngược lại thì thêm mới type R
                    else
                    {
                        var tmpQrcodeVersion = transactionMain.QRCode_Version + 1;
                        WMSB_Transaction_Main modelTypeR = new WMSB_Transaction_Main();
                        modelTypeR.Transac_Type = "R";
                        modelTypeR.Transac_No = "R" + transactionMain.Transac_No;
                        modelTypeR.Transac_Sheet_No = "R" + transactionMain.Transac_Sheet_No;
                        modelTypeR.Transacted_Qty = itemt.output.TransOutQty;
                        modelTypeR.Updated_By = "Emma";
                        modelTypeR.Updated_Time = timeNow;
                        modelTypeR.Missing_No = transactionMain.Missing_No;
                        modelTypeR.Material_ID = transactionMain.Material_ID;
                        modelTypeR.Material_Name = transactionMain.Material_Name;
                        modelTypeR.Purchase_No = transactionMain.Purchase_No;
                        modelTypeR.Rack_Location = transactionMain.Rack_Location;
                        modelTypeR.Purchase_Qty = transactionMain.Purchase_Qty;
                        modelTypeR.QRCode_Version = tmpQrcodeVersion;
                        modelTypeR.QRCode_ID = transactionMain.QRCode_ID;
                        modelTypeR.MO_No = transactionMain.MO_No;
                        modelTypeR.MO_Seq = transactionMain.MO_Seq;
                        modelTypeR.Can_Move = "Y";
                        modelTypeR.Transac_Time = transactionMain.Transac_Time;
                        _repoTransactionMain.Add(modelTypeR);

                        // thêm transaction main cũng phải thêm transaction detail
                        foreach (var itemTypeR in itemt.transactionDetail)
                        {
                            itemTypeR.ID = 0;// ID trong db là tự tăng: dựa vào transaction detail cũ nên thêm mới gán id bằng 0, không cần phải new hết thuộc tính của đổi tượng ra
                            itemTypeR.Transac_No = modelTypeR.Transac_No;
                            itemTypeR.Updated_By = "Emma";
                            itemTypeR.Updated_Time = timeNow;
                            itemTypeR.Trans_Qty = 0;// nếu transaction main là Type R là thì transaction detail của nó gán Trans_Qty = 0
                            var itemModel = _mapper.Map<WMSB_Transaction_Detail>(itemTypeR);
                            _repoTransactionDetail.Add(itemModel);
                        }
                    }

                    // thêm qrcode mới, nếu output ra chưa hết thì thêm qrcode main mới dựa vào cái cũ và update version lên
                    var qrCodeMain = await _repoQRCodeMain.FindAll(x => x.QRCode_ID.Trim() == itemt.output.QrCodeId.Trim()).OrderByDescending(x => x.QRCode_Version).FirstOrDefaultAsync();
                    WMSB_QRCode_Main modelQrCodeMain = new WMSB_QRCode_Main();
                    modelQrCodeMain.QRCode_ID = qrCodeMain.QRCode_ID;
                    modelQrCodeMain.QRCode_Type = qrCodeMain.QRCode_Type;
                    modelQrCodeMain.Receive_No = qrCodeMain.Receive_No;
                    modelQrCodeMain.Valid_Status = "Y";
                    modelQrCodeMain.Is_Scanned = "Y";
                    modelQrCodeMain.Invalid_Date = qrCodeMain.Invalid_Date;
                    modelQrCodeMain.QRCode_Version = qrCodeMain.QRCode_Version + 1;
                    modelQrCodeMain.Updated_Time = timeNow;
                    modelQrCodeMain.Updated_By = "Emma";
                    _repoQRCodeMain.Add(modelQrCodeMain);

                    // Update cho QRCode cũ, Valid_Status =N, Invalid_Date = Ngày mà tạo ra version mới
                    qrCodeMain.Valid_Status = "N";
                    qrCodeMain.Invalid_Date = timeNow;
                    _repoQRCodeMain.Update(qrCodeMain);

                    // thêm qrcodedetail của qrcode mới: thêm qrcode main cũng phải thêm qrcode detail
                    var qrCodeDetails = await _repoQRCodeDetail.FindAll(x => x.QRCode_ID.Trim() == qrCodeMain.QRCode_ID.Trim() && x.QRCode_Version == qrCodeMain.QRCode_Version).ToListAsync();
                    foreach (var itemQrCodeDetail in qrCodeDetails)
                    {
                        itemQrCodeDetail.QID = 0;
                        itemQrCodeDetail.Updated_By = "Emma";
                        itemQrCodeDetail.Updated_Time = timeNow;
                        itemQrCodeDetail.QRCode_Version = modelQrCodeMain.QRCode_Version;
                        itemQrCodeDetail.Qty = itemt.transactionDetail.Where(x => x.Tool_Size == itemQrCodeDetail.Tool_Size && x.Order_Size == itemQrCodeDetail.Order_Size && x.Model_Size == itemQrCodeDetail.Model_Size).FirstOrDefault().Instock_Qty;
                        _repoQRCodeDetail.Add(itemQrCodeDetail);
                    }
                }
            }

            // lưu Db
            return await _repoTransactionMain.SaveAll();
        }
    }
}