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
using Bottom_API.Models;
using Bottom_API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class QRCodeMainService : IQRCodeMainService
    {
        private readonly IQRCodeMainRepository _repoQrcode;
        private readonly IPackingListRepository _repoPacking;
        private readonly IPackingListDetailRepository _repoPackingDetail;
        private readonly IQRCodeDetailRepository _repoQrCodeDetail;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public QRCodeMainService(   IQRCodeMainRepository repoQrcode,
                                    IPackingListRepository repoPacking,
                                    IPackingListDetailRepository repoPackingDetail,
                                    IQRCodeDetailRepository repoQrCodeDetail,
                                    IMapper mapper,
                                    MapperConfiguration configMapper,
                                    ITransactionDetailRepo repoTransactionDetail,
                                    ITransactionMainRepo repoTransactionMain) {
            _repoQrcode = repoQrcode;
            _repoPacking = repoPacking;
            _repoPackingDetail = repoPackingDetail;
            _repoQrCodeDetail = repoQrCodeDetail;
            _mapper = mapper;
            _configMapper = configMapper;
            _repoTransactionDetail = repoTransactionDetail;
            _repoTransactionMain =  repoTransactionMain;
        }
        public Task<bool> Add(QRCode_Main_Dto model)
        {
            throw new System.NotImplementedException();
        }
        public async Task<bool> AddListQRCode(List<string> listReceiveNo)
        {
            var checkCreate = true;
            Random ran = new Random();
            foreach (var item in listReceiveNo)
            {
                // Tạo QrCodeMain để thêm vào database
                var qrCodeDto = new QRCode_Main_Dto();
                var packing = await _repoPacking.GetAll().Where(x => x.Receive_No.Trim() == item.Trim()).FirstOrDefaultAsync();
                packing.Generated_QRCode = "Y";
                int so = ran.Next(100,999);
                string qrCodeId = "B" + packing.MO_No.Trim() + so.ToString();
                qrCodeDto.QRCode_ID = qrCodeId;
                qrCodeDto.Receive_No = packing.Receive_No.Trim();
                qrCodeDto.QRCode_Version = 1;
                qrCodeDto.Valid_Status = "Y";
                qrCodeDto.QRCode_Type = packing.Sheet_Type.Trim();
                await _repoPacking.SaveAll();
                var qrCodeMain = _mapper.Map<WMSB_QRCode_Main>(qrCodeDto);
                _repoQrcode.Add(qrCodeMain);

                // Tạo QrCodeDetail để thêm vào database
                var listPackingDetail = await _repoPackingDetail.GetAll().Where(x => x.Receive_No.Trim() == item.Trim()).ToListAsync();
                foreach (var packingItem in listPackingDetail)
                    {
                        var qrCodeDetailDto = new QRCode_Detail_Dto();
                        qrCodeDetailDto.QRCode_ID = qrCodeId;
                        qrCodeDetailDto.QRCode_Version = 1;
                        qrCodeDetailDto.Order_Size = packingItem.Order_Size;
                        qrCodeDetailDto.Model_Size = packingItem.Model_Size;
                        qrCodeDetailDto.Tool_Size = packingItem.Tool_Size;
                        qrCodeDetailDto.Spec_Size = packingItem.Spec_Size;
                        qrCodeDetailDto.Qty = packingItem.Received_Qty;
                        qrCodeDetailDto.Updated_By = "Emma";
                        var qrCodeDetail = _mapper.Map<WMSB_QRCode_Detail>(qrCodeDetailDto);
                        _repoQrCodeDetail.Add(qrCodeDetail);
                        if (!await _repoQrCodeDetail.SaveAll()) {
                            checkCreate = false;
                            break;
                        }
                    }
            }
            await _repoQrcode.SaveAll();
            return checkCreate;
        }
        public async Task<PagedList<QRCodeMainViewModel>> SearchByPlanNo(PaginationParams param ,FilterQrCodeParam filterParam)
        {
            var listPackingList =  _repoPacking.GetAll()
                .Where( x => x.Receive_Date >= Convert.ToDateTime(filterParam.From_Date + " 00:00:00.000") &&
                        x.Receive_Date <= Convert.ToDateTime(filterParam.To_Date + " 23:59:59.997"));
            var listQrCodeMain = _repoQrcode.GetAll().Where(x => x.Valid_Status.Trim() == "Y");
            if (filterParam.MO_No != null && filterParam.MO_No != string.Empty) {
                listPackingList = listPackingList.Where(x => x.MO_No.Trim() == filterParam.MO_No.Trim());
            }
            var listQrCodeModel = (from x in listQrCodeMain join y in listPackingList
                                    on x.Receive_No.Trim() equals y.Receive_No.Trim()
                                    select new QRCodeMainViewModel() {
                                        QRCode_ID = x.QRCode_ID,
                                        MO_No = y.MO_No,
                                        Receive_No = x.Receive_No,
                                        Receive_Date = y.Receive_Date,
                                        Supplier_ID = y.Supplier_ID,
                                        Supplier_Name = y.Supplier_Name,
                                        T3_Supplier = y.T3_Supplier,
                                        T3_Supplier_Name = y.T3_Supplier_Name,
                                        Subcon_ID = y.Subcon_ID,
                                        Subcon_Name = y.Subcon_Name,
                                        Model_Name = y.Model_Name,
                                        Model_No = y.Model_No,
                                        Article = y.Article,
                                        MO_Seq = y.MO_Seq,
                                        Material_ID = y.Material_ID,
                                        Material_Name = y.Material_Name
                                                    }).Distinct().OrderByDescending(x => x.Receive_Date);
            // var listQrCodeModel = listQrCodeMain
            //     .Join(listPackingList, x => x.Receive_No.Trim(), y=> y.Receive_No.Trim(), (x,y) => new QRCodeMainViewModel
            //     {
            //         QRCode_ID = x.QRCode_ID,
            //         MO_No = y.MO_No,
            //         Receive_No = x.Receive_No,
            //         Receive_Date = y.Receive_Date,
            //         Supplier_ID = y.Supplier_ID,
            //         Supplier_Name = y.Supplier_Name,
            //         T3_Supplier = y.T3_Supplier,
            //         T3_Supplier_Name = y.T3_Supplier_Name,
            //         Subcon_ID = y.Subcon_ID,
            //         Subcon_Name = y.Subcon_Name,
            //         Model_Name = y.Model_Name,
            //         Model_No = y.Model_No,
            //         Article = y.Article,
            //         MO_Seq = y.MO_Seq,
            //         Material_ID = y.Material_ID,
            //         Material_Name = y.Material_Name
            //     });

            return await PagedList<QRCodeMainViewModel>.CreateAsync(listQrCodeModel, param.PageNumber, param.PageSize);
        }
        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<QRCode_Main_Dto>> GetAllAsync()
        {
            return await _repoQrcode.GetAll().ProjectTo<QRCode_Main_Dto>(_configMapper).ToListAsync();
        }

        public QRCode_Main_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<QRCode_Main_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<QRCode_Main_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Update(QRCode_Main_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<QRCodePrint_Dto> GetQrCodePrint(string qrCodeId, int qrCodeVersion)
        {
            var qrCodeModel = _repoQrcode.FindSingle(x => x.QRCode_ID.Trim() == qrCodeId.Trim() && x.QRCode_Version == qrCodeVersion);
            var packingListModel = await _repoPacking.FindAll(x => x.Receive_No.Trim() == qrCodeModel.Receive_No.Trim()).ProjectTo<Packing_List_Dto>(_configMapper).FirstOrDefaultAsync();
            var transactionMainModel = _repoTransactionMain.FindSingle(x => x.QRCode_ID.Trim() == qrCodeModel.QRCode_ID.Trim() && x.QRCode_Version == qrCodeModel.QRCode_Version && (x.Transac_Type.Trim() == "I" || x.Transac_Type.Trim() == "R"));
            var transactionDetailModel = await _repoTransactionDetail.FindAll(x => x.Transac_No.Trim() == transactionMainModel.Transac_No.Trim()).ProjectTo<TransferLocationDetail_Dto>(_configMapper).ToListAsync();
            
            // Lấy ra những thuộc tính cần in
            QRCodePrint_Dto result = new QRCodePrint_Dto();
            result.TransactionDetailByQrCodeId = transactionDetailModel;
            result.PackingListByQrCodeId = packingListModel;
            result.RackLocation = transactionMainModel.Rack_Location;

            return result;
        }

        public async Task<int> GetQrCodeVersionLastest(string qrCodeId)
        {
            var model = await _repoQrcode.FindAll(x => x.QRCode_ID.Trim() == qrCodeId.Trim()).OrderByDescending(x => x.QRCode_Version).FirstOrDefaultAsync();
            return model.QRCode_Version;
        }
    }
}