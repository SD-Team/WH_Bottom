using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Models;
using Bottom_API.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace Bottom_API._Services.Services
{
    public class InputService : IInputService
    {
        private readonly IPackingListRepository _repoPackingList;
        private readonly IQRCodeMainRepository _repoQRCodeMain;
        private readonly IQRCodeDetailRepository _repoQRCodeDetail;
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly IMaterialMissingRepository _repoMaterialMissing;
        private readonly IMaterialPurchaseRepository _repoMatPurchase;
        private readonly IMaterialMissingRepository _repoMatMissing;
        private readonly IMaterialViewRepository _repoMaterialView;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public InputService(
            IPackingListRepository repoPackingList,
            IQRCodeMainRepository repoQRCodeMain,
            IQRCodeDetailRepository repoQRCodeDetail,
            ITransactionMainRepo repoTransactionMain,
            ITransactionDetailRepo repoTransactionDetail,
            IMaterialMissingRepository repoMaterialMissing,
            IMaterialPurchaseRepository repoMatPurchase,
            IMaterialMissingRepository repoMatMissing,
            IMaterialViewRepository repoMaterialView,
            IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoQRCodeMain = repoQRCodeMain;
            _repoQRCodeDetail = repoQRCodeDetail;
            _repoPackingList = repoPackingList;
            _repoTransactionMain = repoTransactionMain;
            _repoTransactionDetail = repoTransactionDetail;
            _repoMaterialMissing = repoMaterialMissing;
            _repoMatMissing = repoMatMissing;
            _repoMatPurchase = repoMatPurchase;
            _repoMaterialView = repoMaterialView;
        }
        public async Task<Transaction_Dto> GetByQRCodeID(object qrCodeID)
        {
            Transaction_Dto model = new Transaction_Dto();
            var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(qrCodeID);
            if (qrCodeModel != null)
            {
                var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                var listQrCodeDetails = await _repoQRCodeDetail.GetByQRCodeIDAndVersion(qrCodeID, qrCodeModel.QRCode_Version);
                decimal? num = 0;
                foreach (var item in listQrCodeDetails)
                {
                    num += item.Qty;
                }

                model.QrCode_Id = qrCodeModel.QRCode_ID.Trim();
                model.Plan_No = packingListModel.MO_No.Trim();
                model.Suplier_No = packingListModel.Supplier_ID.Trim();
                model.Suplier_Name = packingListModel.Supplier_Name.Trim();
                model.Batch = packingListModel.MO_Seq;
                model.Mat_Id = packingListModel.Material_ID.Trim();
                model.Mat_Name = packingListModel.Material_Name.Trim();
                model.Accumated_Qty = num;
                model.Trans_In_Qty = 0;
                model.InStock_Qty = 0;
            }

            return model;
        }

        public async Task<Transaction_Detail_Dto> GetDetailByQRCodeID(object qrCodeID)
        {
            Transaction_Detail_Dto model = new Transaction_Detail_Dto();
            var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(qrCodeID);
            if (qrCodeModel != null && qrCodeModel.Valid_Status == "Y")
            {
                var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                var listQrCodeDetails = await _repoQRCodeDetail.GetByQRCodeIDAndVersion(qrCodeID, qrCodeModel.QRCode_Version);
                decimal? num = 0;
                List<DetailSize> listDetail = new List<DetailSize>();
                foreach (var item in listQrCodeDetails)
                {
                    DetailSize detail = new DetailSize();
                    num += item.Qty;
                    detail.Size = item.Order_Size;
                    detail.Qty = item.Qty;
                    listDetail.Add(detail);
                }
                model.Suplier_No = packingListModel.Supplier_ID;
                model.Suplier_Name = packingListModel.Supplier_Name;
                model.Detail_Size = listDetail;
                model.QrCode_Id = qrCodeModel.QRCode_ID.Trim();
                model.Plan_No = packingListModel.MO_No.Trim();
                model.Batch = packingListModel.MO_Seq;
                model.Mat_Id = packingListModel.Material_ID.Trim();
                model.Mat_Name = packingListModel.Material_Name.Trim();
                model.Accumated_Qty = num;
                model.Trans_In_Qty = 0;
                model.InStock_Qty = 0;
            }
            return model;
        }

        public async Task<bool> CreateInput(Transaction_Detail_Dto model)
        {
            var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(model.QrCode_Id);
            if(qrCodeModel != null && qrCodeModel.Valid_Status == "Y") {
                var listQrCodeDetails = await _repoQRCodeDetail.GetByQRCodeIDAndVersion(qrCodeModel.QRCode_ID, qrCodeModel.QRCode_Version);
                Random ran = new Random();
                int num = ran.Next(100, 999);
                var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                WMSB_Transaction_Main inputModel = new WMSB_Transaction_Main();
                inputModel.Transac_Type = "I";
                inputModel.Transac_No = model.Input_No;
                inputModel.Transac_Time = DateTime.Now;
                inputModel.QRCode_ID = qrCodeModel.QRCode_ID;
                inputModel.QRCode_Version = qrCodeModel.QRCode_Version;
                inputModel.MO_No = packingListModel.MO_No;
                inputModel.MO_Seq = packingListModel.MO_Seq;
                inputModel.Purchase_No = packingListModel.Purchase_No;
                inputModel.Material_ID = packingListModel.Material_ID;
                inputModel.Material_Name = packingListModel.Material_Name;
                inputModel.Purchase_Qty = model.Accumated_Qty;
                inputModel.Transacted_Qty = model.Trans_In_Qty;
                inputModel.Rack_Location = model.Rack_Location;
                inputModel.Can_Move = "Y";
                inputModel.Updated_By = "Emma";
                inputModel.Updated_Time = DateTime.Now;
                _repoTransactionMain.Add(inputModel);

                var i = 0;
                foreach (var item in model.Detail_Size)
                {
                    WMSB_Transaction_Detail inputDetailModel = new WMSB_Transaction_Detail();
                    inputDetailModel.Transac_No = inputModel.Transac_No;
                    inputDetailModel.Tool_Size = listQrCodeDetails[i].Tool_Size;
                    inputDetailModel.Model_Size = listQrCodeDetails[i].Model_Size;
                    inputDetailModel.Order_Size = listQrCodeDetails[i].Order_Size;
                    inputDetailModel.Spec_Size = listQrCodeDetails[i].Spec_Size;
                    inputDetailModel.Qty = listQrCodeDetails[i].Qty;
                    inputDetailModel.Trans_Qty = item.Qty;
                    inputDetailModel.Instock_Qty = item.Qty;
                    inputDetailModel.Untransac_Qty = inputDetailModel.Qty - inputDetailModel.Trans_Qty;
                    inputDetailModel.Updated_By = "Nam";
                    inputDetailModel.Updated_Time = DateTime.Now;
                    _repoTransactionDetail.Add(inputDetailModel);
                    i += 1;
                }
                return await _repoTransactionMain.SaveAll();
            }
            return false;
        }

        public async Task<bool> SubmitInput(List<string> lists)
        {
            Random ran = new Random();
            if(lists.Count > 0) {
                int num = ran.Next(100, 999);
                var Transac_Sheet_No = "IB" + DateTime.Now.ToString("yyyyMMdd") + num.ToString();
                var Missing_No = "BTM" + DateTime.Now.ToString("yyyyMMdd") + num.ToString();
                foreach (var item in lists)
                {
                    WMSB_Transaction_Main model = await _repoTransactionMain.GetByInputNo(item);
                    model.Can_Move = "Y";
                    model.Transac_Sheet_No = Transac_Sheet_No;
                    model.Updated_By = "Nam";
                    model.Updated_Time = DateTime.Now;
                    if(model.Purchase_Qty > model.Transacted_Qty) {
                        model.Missing_No = Missing_No;

                        //Tạo mới record và update status record cũ trong bảng QRCode_Main và QRCode_Detail
                        GenerateNewQrCode(model.QRCode_ID, model.QRCode_Version, item);

                        //Update QrCode Version cho bảng Transaction_Main
                        model.QRCode_Version += 1;

                        // Tạo mới record trong bảng Missing
                        CreateMissing(model.Purchase_No, model.MO_No, model.MO_Seq, model.Material_ID, model.Transac_No, model.Missing_No);
                    }else {
                        WMSB_QRCode_Main qrModel = await _repoQRCodeMain.GetByQRCodeID(model.QRCode_ID);
                        qrModel.Is_Scanned = "Y";
                        _repoQRCodeMain.Update(qrModel);
                    }
                    
                    _repoTransactionMain.Update(model);
                }
                return await _repoTransactionMain.SaveAll();
            }
            return false;
        }

        public async Task<MissingPrint_Dto> GetMaterialPrint(string missingNo)
        {
            var materialMissingModel = await _repoMaterialMissing.FindAll(x => x.Missing_No.Trim() == missingNo.Trim()).ProjectTo<Material_Dto>(_configMapper).FirstOrDefaultAsync();
            var transactionMainModel = _repoTransactionMain.FindSingle(x => x.Missing_No.Trim() == missingNo.Trim() && x.Transac_Type.Trim() == "I");
            var transactionDetailByMissingNo = await _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transactionMainModel.Transac_No.Trim()).ProjectTo<TransferLocationDetail_Dto>(_configMapper).OrderBy(x => x.Tool_Size).ToListAsync();

            // Lấy ra những thuộc tính cần in
            MissingPrint_Dto result = new MissingPrint_Dto();
            result.MaterialMissing = materialMissingModel;
            result.TransactionDetailByMissingNo = transactionDetailByMissingNo;

            return result;
        }
        private void CreateMissing(string Purchase_No, string MO_No, string MO_Seq, string Material_ID, string transacNo, string Missing_No) 
        {
            //Lấy list Material Purchase
            var matPurchase = _repoMatPurchase.GetFactory(Purchase_No, MO_No, MO_Seq, Material_ID);

            //Lấy PackingList
            var packingList = _repoPackingList.GetPackingList(Purchase_No, MO_No, MO_Seq, Material_ID);
            //Lấy danh sách transaction detail
            List<WMSB_Transaction_Detail> listDetails = _repoTransactionDetail.GetListTransDetailByTransacNo(transacNo);
            foreach (var detail in listDetails)
            {
                WMSB_Material_Missing model = new WMSB_Material_Missing();
                model.Missing_No = Missing_No;
                model.Purchase_No = Purchase_No;
                model.MO_No = MO_No;
                model.MO_Seq = MO_Seq;
                model.Material_ID = packingList.Material_ID;
                model.Material_Name = packingList.Material_Name;
                model.Model_No = packingList.Model_No;
                model.Model_Name = packingList.Model_Name;
                model.Article = packingList.Article;
                model.Supplier_ID = packingList.Supplier_ID;
                model.Supplier_Name = packingList.Supplier_Name;
                model.Process_Code = packingList.Subcon_ID;
                model.Subcon_Name = packingList.Subcon_Name;
                model.T3_Supplier = packingList.T3_Supplier;
                model.T3_Supplier_Name = packingList.T3_Supplier_Name;
                model.Order_Size = detail.Order_Size;
                model.Model_Size = detail.Model_Size;
                model.Tool_Size = detail.Tool_Size;
                model.Spec_Size = detail.Spec_Size;
                model.Purchase_Qty = detail.Untransac_Qty;
                model.Accumlated_In_Qty = 0;
                model.Status = "N";
                model.Updated_Time = DateTime.Now;
                model.Updated_By = "Nam";
                foreach (var purchase in matPurchase)
                {
                    if(detail.Order_Size == purchase.Order_Size) {
                        model.Factory_ID = purchase.Factory_ID;
                        model.MO_Qty = purchase.MO_Qty;
                        model.PreBook_Qty = purchase.PreBook_Qty;
                        model.Stock_Qty = purchase.Stock_Qty;
                        model.Require_Delivery = purchase.Require_Delivery;
                        model.Confirm_Delivery = purchase.Confirm_Delivery;
                        model.Custmoer_Part = purchase.Custmoer_Part;
                        model.T3_Purchase_No = purchase.T3_Purchase_No;
                        model.Stage = purchase.Stage;
                        model.Tool_ID = purchase.Tool_ID;
                        model.Tool_Type = purchase.Tool_Type;
                        model.Purchase_Kind = purchase.Purchase_Kind;
                        model.Collect_No = purchase.Collect_No;
                        model.Purchase_Size = purchase.Purchase_Size;
                    }
                }
                _repoMatMissing.Add(model);
            }
        }
        private void GenerateNewQrCode(string qrCodeID, int qrCodeVersion, string transacNo) 
        {
            //Update dòng QRCodeMain cũ
            var qrCodeMain = _repoQRCodeMain.GetByQRCodeIDAndVersion(qrCodeID, qrCodeVersion);
            qrCodeMain.Valid_Status = "N";
            qrCodeMain.Is_Scanned = "Y";
            qrCodeMain.Invalid_Date = DateTime.Now;
            _repoQRCodeMain.Update(qrCodeMain);

            //Thêm QRCode mới và update version lên 
            WMSB_QRCode_Main model = new WMSB_QRCode_Main();
            model.QRCode_ID = qrCodeID;
            model.QRCode_Version = qrCodeMain.QRCode_Version + 1;
            model.QRCode_Type = qrCodeMain.QRCode_Type;
            model.Receive_No = qrCodeMain.Receive_No;
            model.Valid_Status = "Y";
            model.Is_Scanned = "Y";
            model.Updated_By = "Nam";
            model.Updated_Time = DateTime.Now;
            _repoQRCodeMain.Add(model);

            //Lấy danh sách transaction detail
            List<WMSB_Transaction_Detail> listDetails = _repoTransactionDetail.GetListTransDetailByTransacNo(transacNo);

            //Tạo mới các dòng QRCode Detail dựa trên QRcode version mới với Qty tương ứng với Trans_Qty của Transaction_Detail
            foreach (var item in listDetails)
            {
                WMSB_QRCode_Detail detailQRCode = new WMSB_QRCode_Detail();
                detailQRCode.QRCode_ID = qrCodeID;
                detailQRCode.QRCode_Version = model.QRCode_Version;
                detailQRCode.Tool_Size = item.Tool_Size;
                detailQRCode.Model_Size = item.Model_Size;
                detailQRCode.Order_Size = item.Order_Size;
                detailQRCode.Spec_Size = item.Spec_Size;
                detailQRCode.Qty = item.Trans_Qty;
                detailQRCode.Updated_By = "Nam";
                detailQRCode.Updated_Time = DateTime.Now;
                _repoQRCodeDetail.Add(detailQRCode);
            }
        }

        public async Task<PagedList<Transaction_Main_Dto>> FilterQrCodeAgain(PaginationParams param, FilterQrCodeAgainParam filterParam)
        {
            var lists =  _repoTransactionMain.GetAll().Where(x => x.Missing_No != string.Empty && x.Missing_No != null)
                .ProjectTo<Transaction_Main_Dto>(_configMapper);
            if (filterParam.MO_No != null && filterParam.MO_No != "") {
                lists = lists.Where(x => x.MO_No.Trim() == filterParam.MO_No.Trim());
            }
            if (filterParam.Rack_Location != null && filterParam.Rack_Location != "") {
                lists = lists.Where(x => x.Rack_Location.Trim() == filterParam.Rack_Location.Trim());
            }
            if (filterParam.Material_ID != null && filterParam.Material_ID != "") {
                lists = lists.Where(x => x.Material_ID.Trim() == filterParam.Material_ID.Trim());
            }
            lists = lists.OrderByDescending(x => x.Updated_Time);
            return await PagedList<Transaction_Main_Dto>.CreateAsync(lists, param.PageNumber, param.PageSize);
        }

        public async Task<string> FindMaterialName(string materialID)
        {
            if(materialID != null && materialID != "") {
                var materialModel = await _repoMaterialView.GetAll()
                                    .Where(x => x.Mat_.Trim() == materialID.Trim()).FirstOrDefaultAsync();
                return materialModel.Mat__Name;
            } else {
                return "";
            }
        }
    }
}