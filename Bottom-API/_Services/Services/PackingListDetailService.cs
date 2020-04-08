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
    public class PackingListDetailService : IPackingListDetailService
    {
        private readonly IPackingListRepository _repoPacking;
        private readonly IPackingListDetailRepository _repo;
        private readonly IQRCodeMainRepository _repoQrcode;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public PackingListDetailService(    IPackingListDetailRepository repo,
                                            IPackingListRepository repoPacking,
                                            IQRCodeMainRepository repoQrcode,
                                            IMapper mapper,
                                            MapperConfiguration configMapper) {
            _repo = repo;
            _repoPacking = repoPacking;
            _repoQrcode = repoQrcode;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<bool> Add(Packing_List_Detail_Dto model)
        {
            var data = _mapper.Map<WMSB_PackingList_Detail>(model);
            _repo.Add(data);
            return await _repo.SaveAll();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> FindByReceiveNo(string Receive_No)
        {
            var lists = await _repo.GetAll().Where(x => x.Receive_No.Trim() == Receive_No.Trim()).ToListAsync();
            var packingListDetailModel = new List<PackingListDetailViewModel>();
            decimal totalQty = 0;
            foreach (var item in lists)
            {
                var packingItem = new PackingListDetailViewModel();
                packingItem.Receive_No = item.Receive_No;
                packingItem.Order_Size = item.Order_Size;
                packingItem.Model_Size = item.Model_Size;
                packingItem.Tool_Size = item.Tool_Size;
                packingItem.Spec_Size = item.Spec_Size;
                packingItem.MO_Qty = item.MO_Qty;
                packingItem.Purchase_Qty = item.Purchase_Qty;
                packingItem.Received_Qty = item.Received_Qty;
                packingItem.Bal = item.Purchase_Qty - item.Received_Qty;
                totalQty = totalQty + item.Purchase_Qty;
                packingListDetailModel.Add(packingItem);
            }
            var result = new {
                totalQty,
                packingListDetailModel
            };
            return result;
        }

        public async Task<List<object>> FindByQRCodeIDList(List<string> data)
        {
            var listPackingList =  _repoPacking.GetAll();
            var listQrCodeMain = _repoQrcode.GetAll();
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
            var objectResult = new List<object>();
            foreach (var item in data)
            {  
                var object1 = await this.FindByReceiveNo(item);
                var qrCodeMainItem = await listQrCodeModel.Where(x => x.QRCode_ID.Trim() == item.Trim()).FirstOrDefaultAsync();
                var objectItem = new {
                    object1,
                    qrCodeMainItem
                };
                objectResult.Add(objectItem);
            }
            return objectResult;
        }

        public async Task<List<Packing_List_Detail_Dto>> GetAllAsync()
        {
            return await _repo.GetAll().ProjectTo<Packing_List_Detail_Dto>(_configMapper).ToListAsync();
        }

        public Packing_List_Detail_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Packing_List_Detail_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Packing_List_Detail_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Packing_List_Detail_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}