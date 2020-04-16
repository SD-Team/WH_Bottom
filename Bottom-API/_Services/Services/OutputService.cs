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

        // public Task<bool> CheckedOutput(Output_Dto output)
        // {
        //     return false;
        //     // // Tìm ra TransactionMain theo id
        //     // var transactionMain = _repoTransactionMain.FindSingle(x => x.ID == output.Id);
        //     // // tạo biến lấy ra Transac_No của TransactionMain
        //     // var transacNo = transactionMain.Transac_No;

        //     // // Thêm TransactionMain mới dựa vào TransactionMain cũ: thêm mới chỉ thay đổi mấy trường dưới còn lại giữ nguyên
        //     // transactionMain.ID = 0; // Trong DB có identity tự tăng
        //     // transactionMain.Transac_Type = "O";
        //     // transactionMain.Can_Move = "Y";
        //     // transactionMain.Rack_Location = item.ToLocation;
        //     // transactionMain.Updated_By = item.UpdateBy;
        //     // transactionMain.Updated_Time = DateTime.Now;
        //     // transactionMain.Transac_Time = item.TransacTime;
        //     // transactionMain.Transac_No = item.TransferNo;
        //     // transactionMain.Transac_Sheet_No = item.TransferNo;
        //     // _repoTransactionMain.Add(transactionMain);

        //     // // Thêm TransactionDetail mới dựa vào TransactionDetail của TransactionMain cũ(có bao nhiêu TransactionDetail cũ là thêm bấy nhiêu TransactionDetail mới): chỉ thay đổi Transac_No thành của TransactionMain mới
        //     // var transactionDetails = await _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transacNo.Trim()).ToListAsync();
        //     // foreach (var transactionDetail in transactionDetails)
        //     // {
        //     //     // thêm mới chỉ thay đổi mấy trường dưới, còn lại giữ nguyên
        //     //     transactionDetail.ID = 0; // Trong DB có identity tự tăng
        //     //     transactionDetail.Transac_No = item.TransferNo;
        //     //     transactionDetail.Updated_Time = DateTime.Now;
        //     //     transactionDetail.Updated_By = item.UpdateBy;
        //     //     _repoTransactionDetail.Add(transactionDetail);
        //     // }
        //     // return await _repoTransactionMain.SaveAll();
        // }

        public async Task<List<Output_Dto>> GetByQrCodeId(string qrCodeId)
        {
            List<Output_Dto> listOuput = new List<Output_Dto>();
            var materialSheetSize = await _repoMaterialSheetSize.FindAll(x => x.Sheet_No.Trim() == qrCodeId.Trim()).FirstOrDefaultAsync();
            if (materialSheetSize != null)
            {
                var transactionModel = await _repoTransactionMain.FindAll(x => x.MO_No.Trim() == materialSheetSize.Manno.Trim() && x.MO_Seq.Trim() == materialSheetSize.Batch.Trim() && x.Material_ID == materialSheetSize.Material_ID && x.Can_Move == "Y").ToListAsync();
                
                foreach (var item in transactionModel)
                {
                    Output_Dto output = new Output_Dto();
                    output.Id = item.ID;
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
                    output.SupplierName = packingListModel.Supplier_Name.Trim();
                    output.SupplierNo = packingListModel.Supplier_ID.Trim();

                    listOuput.Add(output);
                }
            }

            return listOuput.OrderBy(x => x.InStockQty).ToList();
        }

        public Task<List<OutputDetail_Dto>> GetDetailOutput(string transacNo)
        {
            throw new NotImplementedException();
        }
    }
}