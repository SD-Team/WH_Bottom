using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class TransferLocationService : ITransferLocationService
    {
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly IQRCodeMainRepository _repoQRCodeMain;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IPackingListRepository _repoPackingList;
        private readonly IMaterialViewRepository _repoMaterialView;

        public TransferLocationService(
            ITransactionMainRepo repoTransactionMain,
            ITransactionDetailRepo repoTransactionDetail,
            IQRCodeMainRepository repoQRCodeMain,
            IMapper mapper,
            MapperConfiguration configMapper,
            IPackingListRepository repoPackingList,
            IMaterialViewRepository repoMaterialView)
        {
            _configMapper = configMapper;
            _repoPackingList = repoPackingList;
            _mapper = mapper;
            _repoTransactionMain = repoTransactionMain;
            _repoTransactionDetail = repoTransactionDetail;
            _repoQRCodeMain = repoQRCodeMain;
            _repoMaterialView = repoMaterialView;
        }
        public async Task<TransferLocation_Dto> GetByQrCodeId(object qrCodeId)
        {
            TransferLocation_Dto model = new TransferLocation_Dto();
            // Lấy ra TransactionMain cùng QRCode_ID và Can_Move == "Y" và QRCode_Version mới nhất
            var transactionModel = await _repoTransactionMain.FindAll(x => x.QRCode_ID.Trim() == qrCodeId.ToString().Trim() && x.Can_Move == "Y" && (x.Transac_Type.Trim() == "I" || x.Transac_Type.Trim() == "M" || x.Transac_Type.Trim() == "R")).OrderByDescending(x => x.QRCode_Version).FirstOrDefaultAsync();
            var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(qrCodeId);
            if (transactionModel != null)
            {
                var packingListModel = await _repoPackingList.FindAll().Where(x => x.MO_No.Trim() == transactionModel.MO_No.Trim()).FirstOrDefaultAsync();
                var materialPurchaseModel = await _repoMaterialView.FindAll().Where(x => x.Plan_No.Trim() == transactionModel.MO_No.Trim() && x.Purchase_No.Trim() == transactionModel.Purchase_No.Trim() && x.Mat_.Trim() == transactionModel.Material_ID.Trim()).FirstOrDefaultAsync();
                model.Id = transactionModel.ID;
                model.QrCodeId = transactionModel.QRCode_ID.Trim();
                model.TransferNo = "TB" + DateTime.Now.ToString("yyyyMMdd") + "001";
                model.PlanNo = transactionModel.MO_No.Trim();
                model.ReceiveNo = qrCodeModel.Receive_No.Trim();
                model.Batch = transactionModel.MO_Seq;
                model.MatId = transactionModel.Material_ID.Trim();
                model.MatName = transactionModel.Material_Name.Trim();
                model.FromLocation = transactionModel.Rack_Location.Trim();
                model.Qty = _repoTransactionDetail.GetQtyByTransacNo(transactionModel.Transac_No);
                model.UpdateBy = "Emma";
                model.TransacTime = DateTime.Now;
                model.ModelName = packingListModel.Model_Name;
                model.ModelNo =  packingListModel.Model_No;
                model.Article = packingListModel.Article;
                model.CustmoerPart = materialPurchaseModel.Custmoer_Part;
                model.CustmoerName = materialPurchaseModel.Custmoer_Name;
            }

            return model;
        }

        public async Task<List<TransferLocationDetail_Dto>> GetDetailTransaction(string transacNo)
        {
            // lấy ra tất cả transaction detail dựa vào transacno
            var model = _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transacNo.Trim());
            var data = await model.ProjectTo<TransferLocationDetail_Dto>(_configMapper).OrderBy(x => x.Tool_Size).ToListAsync();
            return data;
        }

        public async Task<PagedList<TransferLocation_Dto>> Search(TransferLocationParam transferLocationParam, PaginationParams paginationParams)
        {
            DateTime t1 = Convert.ToDateTime(transferLocationParam.FromDate);
            DateTime t2 = DateTime.Parse(transferLocationParam.ToDate + " 23:59:59");// ép về kiểu ngày truyền vào và giờ là 23h59'
            var model = _repoTransactionMain.FindAll(x => x.Transac_Time >= t1 && x.Transac_Time <= t2 && x.Transac_Type != "R");

            if (transferLocationParam.Status != string.Empty && transferLocationParam.Status != null)
            {
                model = model.Where(x => x.Transac_Type.Trim() == transferLocationParam.Status.Trim());
            }

            var data = model.Select(x => new TransferLocation_Dto {
                Batch = x.MO_Seq,
                FromLocation = "",
                Qty = _repoTransactionDetail.GetQtyByTransacNo(x.Transac_No),
                UpdateBy = x.Updated_By,
                TransferNo = x.Transac_No.Trim(),
                TransacTime = x.Transac_Time,
                PlanNo = x.MO_No,
                MatId = x.Material_ID,
                MatName = x.Material_Name,
                ToLocation = x.Rack_Location,
                Id = x.ID,
                TransacType = x.Transac_Type.Trim()
            });
            return await PagedList<TransferLocation_Dto>.CreateAsync(data, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<bool> SubmitTransfer(List<TransferLocation_Dto> lists)
        {
            DateTime timeNow = DateTime.Now;
            if (lists.Count > 0)
            {
                foreach (var item in lists)
                {
                    // Tìm ra TransactionMain theo id
                    var transactionMain = _repoTransactionMain.FindSingle(x => x.ID == item.Id);
                    // tạo biến lấy ra Transac_No của TransactionMain
                    var transacNo = transactionMain.Transac_No;

                    // Update TransactionMain cũ:  Can_Move = "N"
                    transactionMain.Can_Move = "N";
                    _repoTransactionMain.Update(transactionMain);
                    await _repoTransactionMain.SaveAll();

                    // Thêm TransactionMain mới dựa vào TransactionMain cũ: thêm mới chỉ thay đổi mấy trường dưới còn lại giữ nguyên
                    transactionMain.ID = 0; // Trong DB có identity tự tăng
                    transactionMain.Transac_Type = "M";
                    transactionMain.Can_Move = "Y";
                    transactionMain.Rack_Location = item.ToLocation;
                    transactionMain.Updated_By = item.UpdateBy;
                    transactionMain.Updated_Time = timeNow;
                    transactionMain.Transac_Time = item.TransacTime;
                    transactionMain.Transac_No = item.TransferNo;
                    transactionMain.Transac_Sheet_No = item.TransferNo;
                    _repoTransactionMain.Add(transactionMain);

                    // Thêm TransactionDetail mới dựa vào TransactionDetail của TransactionMain cũ(có bao nhiêu TransactionDetail cũ là thêm bấy nhiêu TransactionDetail mới): chỉ thay đổi Transac_No thành của TransactionMain mới
                    var transactionDetails = await _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transacNo.Trim()).ToListAsync();
                    foreach (var transactionDetail in transactionDetails)
                    {
                        // thêm mới chỉ thay đổi mấy trường dưới, còn lại giữ nguyên
                        transactionDetail.ID = 0; // Trong DB có identity tự tăng
                        transactionDetail.Transac_No = item.TransferNo;
                        transactionDetail.Updated_Time = timeNow;
                        transactionDetail.Updated_By = item.UpdateBy;
                        _repoTransactionDetail.Add(transactionDetail);
                    }

                }
                return await _repoTransactionMain.SaveAll();
            }

            return false;
        }
    }
}