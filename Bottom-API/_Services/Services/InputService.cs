using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Helpers;

namespace Bottom_API._Services.Services
{
    public class InputService : IInputService
    {
        private readonly IPackingListRepository _repoPackingList;
        private readonly IQRCodeMainRepository _repoQRCodeMain;
        private readonly IQRCodeDetailRepository _repoQRCodeDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public InputService(
            IPackingListRepository repoPackingList, 
            IQRCodeMainRepository repoQRCodeMain,
            IQRCodeDetailRepository repoQRCodeDetail,
            IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoQRCodeMain = repoQRCodeMain;
            _repoQRCodeDetail = repoQRCodeDetail;
            _repoPackingList = repoPackingList;

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
                model.Suplier_Name = packingListModel.Subcon_Name.Trim();
                model.Batch = packingListModel.MO_Seq;
                model.Mat_Id = packingListModel.Material_ID.Trim();
                model.Mat_Name = packingListModel.Material_Name.Trim();
                model.Accumated_Qty = num;
                model.Trans_In_Qty = 0;
                model.InStock_Qty = 0;
            }
            
            return model;
        }
        public Task<bool> Add(Transaction_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Transaction_Dto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Transaction_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }



        public Task<PagedList<Transaction_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Transaction_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Transaction_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}