using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public TransferLocationService(
            ITransactionMainRepo repoTransactionMain,
            ITransactionDetailRepo repoTransactionDetail,
            IQRCodeMainRepository repoQRCodeMain,
            IMapper mapper,
            MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoTransactionMain = repoTransactionMain;
            _repoTransactionDetail = repoTransactionDetail;
            _repoQRCodeMain = repoQRCodeMain;
        }
        public async Task<TransferLocation_Dto> GetByQrCodeId(object qrCodeId)
        {
            TransferLocation_Dto model = new TransferLocation_Dto();
            // Lấy ra thằng nào có cùng QRCode_ID và Can_Move == "Y" và QRCode_Version mới nhất
            var transctionModel = await _repoTransactionMain.FindAll(x => x.QRCode_ID.Trim() == qrCodeId.ToString().Trim() && x.Can_Move == "Y").OrderByDescending(x => x.QRCode_Version).FirstOrDefaultAsync();
            var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(qrCodeId);
            if (transctionModel != null)
            {
                Random ran = new Random();
                int num = ran.Next(100, 999);
                model.Id = transctionModel.ID;
                model.QrCodeId = transctionModel.QRCode_ID.Trim();
                model.TransferNo = "TB" + DateTime.Now.ToString("yyyyMMdd") + num.ToString();
                model.PlanNo = transctionModel.MO_No.Trim();
                model.ReceiveNo = qrCodeModel.Receive_No.Trim();
                model.Batch = transctionModel.MO_Seq;
                model.MatId = transctionModel.Material_ID.Trim();
                model.MatName = transctionModel.Material_Name.Trim();
                model.FromLocation = transctionModel.Rack_Location.Trim();
                model.Qty = _repoTransactionDetail.GetQtyByTransacNo(transctionModel.Transac_No);
                model.UpdateBy = "Emma";
            }

            return model;
        }

        public async Task<bool> SubmitTransfer(List<TransferLocation_Dto> lists)
        {
            if (lists.Count > 0)
            {
                foreach (var item in lists)
                {
                    var transactionMain = await _repoTransactionMain.FindAll(x => x.ID == item.Id).FirstOrDefaultAsync();
                    // Update cha thằng cũ: Transac_Type = "M", Can_Move = "N"
                    transactionMain.Transac_Type = "M";
                    transactionMain.Can_Move = "N";
                    transactionMain.Updated_Time = DateTime.Now;
                    _repoTransactionMain.Update(transactionMain);

                    // Thêm thằng con bằng số thằng con của thằng cha cũ, chỉ thay đổi Transac_No thành của thằng mới
                    var transactionDetails = await _repoTransactionDetail.FindAll(x => x.Transac_No == transactionMain.Transac_No).ToListAsync();
                    foreach (var transactionDetail in transactionDetails)
                    {
                        transactionDetail.ID = 0;
                        transactionDetail.Transac_No = item.TransferNo;
                        transactionDetail.Updated_Time = DateTime.Now;
                        transactionDetail.Updated_By = item.UpdateBy;
                        _repoTransactionDetail.Add(transactionDetail);
                    }

                    await _repoTransactionDetail.SaveAll();

                    // Thêm thằng cha mới mới dựa vào thằng cha cũ
                    transactionMain.ID = 0;
                    transactionMain.Transac_Type = "I";
                    transactionMain.Can_Move = "Y";
                    transactionMain.Rack_Location = item.FromLocation;
                    transactionMain.Updated_By = item.UpdateBy;
                    transactionMain.Transac_Time = DateTime.Now;
                    transactionMain.Updated_Time = DateTime.Now;
                    transactionMain.Transac_No = item.TransferNo;
                    _repoTransactionMain.Add(transactionMain);
                }
                return await _repoTransactionMain.SaveAll();
            }

            return false;
        }
    }
}