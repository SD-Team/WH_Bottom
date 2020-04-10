using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Models;
using Bottom_API.Helpers;
using System;

namespace Bottom_API._Services.Services
{
    public class InputService : IInputService
    {
        private readonly IPackingListRepository _repoPackingList;
        private readonly IQRCodeMainRepository _repoQRCodeMain;
        private readonly IQRCodeDetailRepository _repoQRCodeDetail;
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public InputService(
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
        public async Task<Transaction_Dto> GetByQRCodeID(object qrCodeID)
        {
            Transaction_Dto model = new Transaction_Dto();
            var qrCodeModel = await _repoQRCodeMain.GetByQRCodeID(qrCodeID);
            if(qrCodeModel != null) {
                var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                var listQrCodeDetails = await _repoQRCodeDetail.GetByQRCodeID(qrCodeID);
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
            if (qrCodeModel != null)
            {
                var packingListModel = await _repoPackingList.GetByReceiveNo(qrCodeModel.Receive_No);
                var listQrCodeDetails = await _repoQRCodeDetail.GetByQRCodeID(qrCodeID);
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
            if(qrCodeModel != null) {
                var listQrCodeDetails = await _repoQRCodeDetail.GetByQRCodeID(qrCodeModel.QRCode_ID);
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
                inputModel.Transacted_Qty = model.Trans_In_Qty;
                inputModel.Rack_Location = model.Rack_Location;
                inputModel.Can_Move = "Y";
                inputModel.Updated_By = "Emma";
                inputModel.Updated_Time = DateTime.Now;
                _repoTransactionMain.Add(inputModel);

                var i = 0;
                foreach (var item in model.Detail_Size )
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
                    inputDetailModel.Updated_By = "Emma";
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
                foreach (var item in lists)
                {
                    int num = ran.Next(100, 999);
                    WMSB_Transaction_Main model = await _repoTransactionMain.GetByInputNo(item);
                    model.Can_Move = "Y";
                    model.Transac_Sheet_No = "IB" + DateTime.Now.ToString("yyyyMMdd") + num.ToString();
                    model.Updated_By = "Nam";
                    model.Updated_Time = DateTime.Now;
                    _repoTransactionMain.Update(model);
                }
                return await _repoTransactionMain.SaveAll();
            }

            return false;
        }
    }
}