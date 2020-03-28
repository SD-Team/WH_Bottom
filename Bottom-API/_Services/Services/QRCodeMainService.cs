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
        private readonly IQRCodeMainRepository _repoQrCodeMain;
        private readonly IPackingListDetailRepository _repoPackingDetail;
        private readonly IQRCodeDetailRepository _repoQrCodeDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public QRCodeMainService(   IQRCodeMainRepository repoQrcode,
                                    IPackingListRepository repoPacking,
                                    IQRCodeMainRepository repoQrCodeMain,
                                    IPackingListDetailRepository repoPackingDetail,
                                    IQRCodeDetailRepository repoQrCodeDetail,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repoQrcode = repoQrcode;
            _repoPacking = repoPacking;
            _repoQrCodeMain = repoQrCodeMain;
            _repoPackingDetail = repoPackingDetail;
            _repoQrCodeDetail = repoQrCodeDetail;
            _mapper = mapper;
            _configMapper = configMapper;
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
                string qrCodeId = "B" + packing.MO_No + so.ToString();
                qrCodeDto.QRCode_ID = qrCodeId;
                qrCodeDto.Receive_No = packing.Receive_No;
                qrCodeDto.QRCode_Version = 1;
                qrCodeDto.Valid_Status = "N";
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
        public async Task<PagedList<QRCodeMainViewModel>> SearchByPlanNo(PaginationParams param ,QRCodeSearchViewModel dataSearch)
        {
            var listPackingList =  _repoPacking.GetAll()
                .Where( x => x.Receive_Date >= Convert.ToDateTime(dataSearch.From_Date + " 00:00") &&
                        x.Receive_Date <= Convert.ToDateTime(dataSearch.To_Date + " 00:00"));
            var listQrCodeMain = _repoQrcode.GetAll();
            if (dataSearch.MO_No != null && dataSearch.MO_No != "") {
                listPackingList = listPackingList.Where(x => x.MO_No.Trim() == dataSearch.MO_No.Trim());
            }
            var listQrCodeModel = listQrCodeMain
                .Join(listPackingList, x => x.Receive_No.Trim(), y=> y.Receive_No.Trim(), (x,y) => new QRCodeMainViewModel
                {
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
                });

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
    }
}