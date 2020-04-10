using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Helpers;

namespace Bottom_API._Services.Services
{
    public class TransferLocationService : ITransferLocationService
    {
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public TransferLocationService(
            ITransactionMainRepo repoTransactionMain,
            ITransactionDetailRepo repoTransactionDetail,
            IMapper mapper,
            MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoTransactionMain = repoTransactionMain;
            _repoTransactionDetail = repoTransactionDetail;

        }
        public async Task<TransferLocation_Dto> GetByQrCodeId(object qrCodeId)
        {
            TransferLocation_Dto model = new TransferLocation_Dto();
            var transctionModel = await _repoTransactionMain.GetByQrCodeId(qrCodeId);
            if (transctionModel != null)
            {
                model.QrCodeId = transctionModel.QRCode_ID.Trim();
                model.TransferNo = transctionModel.Transac_No.Trim();
                model.PlanNo = transctionModel.MO_No.Trim();
                model.ReceiveNo = "";
                model.Batch = transctionModel.MO_Seq;
                model.MatId = transctionModel.Material_ID.Trim();
                model.MatName = transctionModel.Material_Name.Trim();
                model.FromLocation = transctionModel.Rack_Location.Trim();
                model.Qty = 0;
            }

            return model;
        }
    }
}