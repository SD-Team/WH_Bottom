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
        private readonly IPackingListRepository _repoPackingList;
        private readonly IPackingListDetailRepository _repoPackingListDetail;
        private readonly IMaterialPurchaseRepository _repoMaterialPurchase;
        private readonly IQRCodeMainRepository _repoQrcode;
        private readonly ITransactionMainRepo _repoTransactionMain;
        private readonly ITransactionDetailRepo _repoTransactionDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public PackingListDetailService(    IPackingListDetailRepository repoPackingListDetail,
                                            IPackingListRepository repoPackingList,
                                            IQRCodeMainRepository repoQrcode,
                                            IMaterialPurchaseRepository repoMaterialPurchase,
                                            ITransactionMainRepo repoTransactionMain,
                                            ITransactionDetailRepo repoTransactionDetail,
                                            IMapper mapper,
                                            MapperConfiguration configMapper) {
            _repoPackingListDetail = repoPackingListDetail;
            _repoPackingList = repoPackingList;
            _repoMaterialPurchase = repoMaterialPurchase;
            _repoQrcode = repoQrcode;
            _repoTransactionMain = repoTransactionMain;
            _repoTransactionDetail = repoTransactionDetail;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<bool> Add(Packing_List_Detail_Dto model)
        {
            var data = _mapper.Map<WMSB_PackingList_Detail>(model);
            _repoPackingListDetail.Add(data);
            return await _repoPackingListDetail.SaveAll();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }


        // ---Phần in QrCodeId khi vào Menu 2.QrGenerate => Print (Material Form)-------//
        public async Task<object> FindByQrCodeID(QrCodeIDVersion data)
        {
            var qrCodeMan = await _repoQrcode.GetAll()
                    .Where(x => x.QRCode_ID.Trim() == data.QRCode_ID.Trim() &&
                        x.QRCode_Version == data.QRCode_Version).FirstOrDefaultAsync();
            var packingList = await _repoPackingList.GetAll().ToListAsync();
            // Tìm kiếm Purchase và Sheet_Type của qrcodeid với version đó.
            var packingListFind = packingList
                .Where(x => x.Receive_No.Trim() == qrCodeMan.Receive_No.Trim()).FirstOrDefault();
            // Tìm List ReceiveNo tương ứng với Purchase và Sheet_Type ở trên        
            var ReceiveNoList = packingList.Where(x => x.Sheet_Type.Trim() == packingListFind.Sheet_Type.Trim() &&
                x.Purchase_No.Trim() == packingListFind.Purchase_No.Trim()).Select(x => x.Receive_No).ToList();
            
            var packingListDetailAll = await _repoPackingListDetail.GetAll().ToListAsync();
            var packingDetailList = new List<WMSB_PackingList_Detail>();
            foreach (var item in ReceiveNoList)
            {
                var packingdetailitem = packingListDetailAll.Where(x => x.Receive_No.Trim() == item.Trim()).ToList();
                packingDetailList.AddRange(packingdetailitem);
            }

            // Gộp theo từng tool size và tính tổng Purchase, Received Qty theo tool size đó.
            var BalByToolSize = packingDetailList.GroupBy(x => x.Tool_Size).Select(x => new {
                Tool_Size = x.FirstOrDefault().Tool_Size,
                Bal = x.Sum(cl => cl.Purchase_Qty)/(ReceiveNoList.Count()) - x.Sum(cl => cl.Received_Qty)
            });

            var packingListDetailModel = new List<PackingListDetailViewModel>();
            var packingListDetailModel1 = new List<PackingListDetailViewModel>();
            var packingListDetailModel2 = new List<PackingListDetailViewModel>();
            var packingListDetailModel3 = new List<PackingListDetailViewModel>();
            decimal? totalPQty = 0;
            decimal? totalRQty = 0;

            var lists = packingListDetailAll
                .Where(x => x.Receive_No.Trim() == qrCodeMan.Receive_No.Trim()).ToList();
            foreach (var item in lists)
            {
                    var packingItem1 = new PackingListDetailViewModel();
                    packingItem1.Receive_No = item.Receive_No;
                    packingItem1.Order_Size = item.Order_Size;
                    packingItem1.Model_Size = item.Model_Size;
                    packingItem1.Tool_Size = item.Tool_Size;
                    packingItem1.Spec_Size = item.Spec_Size;
                    packingItem1.MO_Qty = item.MO_Qty;
                    packingItem1.Purchase_Qty = item.Purchase_Qty;
                    packingItem1.Received_Qty = item.Received_Qty;
                    packingItem1.Act = 0;
                    foreach (var itemByToolSize in BalByToolSize)
                    {
                        if (itemByToolSize.Tool_Size.Trim() == item.Tool_Size.Trim()) {
                            packingItem1.Bal = itemByToolSize.Bal;
                        }
                    }
                    totalPQty = totalPQty + item.Purchase_Qty;
                    totalRQty = totalRQty + item.Received_Qty;
                    packingListDetailModel.Add(packingItem1);
            }

            //----------------- Xử lý mảng dữ liệu cho 1 số dòng cùng tool size.----------------//
              // List các Tool Size mà có nhiều Order Size trong bảng Packing List Detail
            var toolSizeMoreOrderSize = lists.Where(x => x.Tool_Size.Trim() != x.Order_Size.Trim()).Select(x => x.Tool_Size).Distinct().ToList();
            if(toolSizeMoreOrderSize.Count() > 0) {
                foreach (var itemToolSize in toolSizeMoreOrderSize)
                {
                    var model1 = packingListDetailModel.Where(x => x.Tool_Size.Trim() == itemToolSize.Trim())
                        .GroupBy(x => x.Tool_Size).Select(x => new {
                            Purchase_Qty = x.Sum(cl => cl.Purchase_Qty),
                            Received_Qty = x.Sum(cl => cl.Received_Qty),
                            Act = x.Sum(cl => cl.Act),
                            Bal = x.First().Bal
                        }).FirstOrDefault();
                    var packingDetailByToolSize = packingListDetailModel
                        .Where(x => x.Tool_Size.Trim() == itemToolSize.Trim()).ToList();
                    for (var i = 0; i < packingDetailByToolSize.Count; i ++)
                    {
                        if( i != 0) {
                                packingDetailByToolSize[i].Purchase_Qty = null;
                                packingDetailByToolSize[i].Received_Qty = null;
                                packingDetailByToolSize[i].Act = null;
                                packingDetailByToolSize[i].Bal = null;
                        } else {
                            packingDetailByToolSize[i].Purchase_Qty = model1.Purchase_Qty;
                            packingDetailByToolSize[i].Received_Qty = model1.Received_Qty;
                            packingDetailByToolSize[i].Act = model1.Act;
                            packingDetailByToolSize[i].Bal = model1.Bal;
                        }
                    }
                }
            }

            var count= packingListDetailModel.Count();
            if(count > 0 && count <=8) {
                packingListDetailModel1 = packingListDetailModel;
            } else if (count > 8 && count <= 16) {
                for (int i = 0; i < 8; i++)
                {
                    packingListDetailModel1.Add(packingListDetailModel[i]);
                }
                for (int i = 8; i < count; i++)
                {
                    packingListDetailModel2.Add(packingListDetailModel[i]);
                }
            } else if(count > 16) {
                for (int i = 0; i < 8; i++)
                {
                    packingListDetailModel1.Add(packingListDetailModel[i]);
                }
                for (int i = 8; i < 16; i++)
                {
                    packingListDetailModel2.Add(packingListDetailModel[i]);
                }
                for (int i = 16; i < count; i++)
                {
                    packingListDetailModel3.Add(packingListDetailModel[i]);
                }
            }
            // Lấy dữ liệu show phần Suggested Location Material Form
            var transactionMain = await _repoTransactionMain.GetAll().ToListAsync();
            var transactionMain1 = transactionMain.Where(x => x.QRCode_ID.Trim() == data.QRCode_ID.Trim() &&
                    (x.Transac_Type == "I" || x.Transac_Type == "M" || x.Transac_Type == "R") &&
                    x.Can_Move.Trim() == "Y");
                    var suggestedReturn1 = transactionMain1.Select(x => new {
                        rack_Location = x.Rack_Location
                    }).Distinct().ToList();
             // Lấy dữ liệu show phần Suggested Location Sorting Form
            var transactionMain2 = transactionMain
                    .Where(x => x.QRCode_ID.Trim() == data.QRCode_ID.Trim() &&
                    x.Transac_Type == "I" &&
                    x.Can_Move.Trim() == "Y");
                var suggestedReturn2 = transactionMain2.Select(x => new {
                        rack_Location = x.Rack_Location
                    }).Distinct().ToList();
                var result = new {
                    totalPQty,
                    totalRQty,
                    packingListDetailModel1, 
                    packingListDetailModel2,
                    packingListDetailModel3,
                    suggestedReturn1,
                    suggestedReturn2,
                };
                return result;
            }

        // ---Phần in QrCodeId sau khi Input (Sorting Form) ----------------------------//
        public async Task<object> FindByQrCode(string qrCodeId)
        {
            var trans = await _repoTransactionMain.GetAll()
                .Where(x => x.QRCode_ID.Trim() == qrCodeId.Trim())
                .OrderByDescending(x => x.QRCode_Version).FirstOrDefaultAsync();
            var versionNew = trans.QRCode_Version;

            var qrCodeMan = await _repoQrcode.GetAll()
                    .Where(x => x.QRCode_ID.Trim() == qrCodeId.Trim() &&
                        x.QRCode_Version == versionNew).FirstOrDefaultAsync();    
            var packingListDetailModel = new List<PackingListDetailViewModel>();
            var packingListDetailModel1 = new List<PackingListDetailViewModel>();
            var packingListDetailModel2 = new List<PackingListDetailViewModel>();
            var packingListDetailModel3 = new List<PackingListDetailViewModel>();
            decimal? totalPQty = 0;
            decimal? totalRQty = 0;
            var transaction = await _repoTransactionMain.GetAll().Where(x => x.QRCode_ID.Trim() == qrCodeId.Trim() &&
                    x.QRCode_Version == versionNew).FirstOrDefaultAsync();
            var transactionDetails = await _repoTransactionDetail.GetAll()
                .Where(x => x.Transac_No.Trim() == transaction.Transac_No.Trim()).ToListAsync();
            
            var lists = await _repoPackingListDetail.GetAll()
                .Where(x => x.Receive_No.Trim() == qrCodeMan.Receive_No.Trim()).ToListAsync();
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
                foreach (var item1 in transactionDetails)
                {
                    if (item1.Tool_Size.Trim() == item.Tool_Size.Trim() && item1.Order_Size.Trim() == item.Order_Size.Trim()) {
                        packingItem.Received_Qty = item1.Qty;
                        packingItem.Act = item1.Instock_Qty;
                        packingItem.Bal = item1.Untransac_Qty;
                        totalRQty = totalRQty + item1.Instock_Qty;
                    }
                }
                // packingItem.Bal = item.Purchase_Qty - item.Received_Qty;
                totalPQty = totalPQty + item.Purchase_Qty;
                // totalRQty = totalRQty + item.Received_Qty;
                packingListDetailModel.Add(packingItem);
            }

             //----------------- Xử lý mảng dữ liệu cho 1 số dòng cùng tool size.----------------//
               // List các Tool Size mà có nhiều Order Size trong bảng Packing List Detail
            var toolSizeMoreOrderSize = lists.Where(x => x.Tool_Size.Trim() != x.Order_Size.Trim()).Select(x => x.Tool_Size).Distinct().ToList();
            if(toolSizeMoreOrderSize.Count() > 0) {
                foreach (var itemToolSize in toolSizeMoreOrderSize)
                {
                    var model1 = packingListDetailModel.Where(x => x.Tool_Size.Trim() == itemToolSize.Trim())
                        .GroupBy(x => x.Tool_Size).Select(x => new {
                            Purchase_Qty = x.Sum(cl => cl.Purchase_Qty),
                            Received_Qty = x.Sum(cl => cl.Received_Qty),
                            Act = x.Sum(cl => cl.Act),
                            Bal = x.Sum(cl => cl.Bal)
                        }).FirstOrDefault();
                    var packingListByToolSize = packingListDetailModel
                        .Where(x => x.Tool_Size.Trim() == itemToolSize.Trim()).ToList();
                    for (var i = 0; i < packingListByToolSize.Count; i ++)
                    {
                        if(i != 0) {
                            packingListByToolSize[i].Purchase_Qty = null;
                            packingListByToolSize[i].Received_Qty = null;
                            packingListByToolSize[i].Act = null;
                            packingListByToolSize[i].Bal = null;
                        } else {
                            packingListByToolSize[i].Purchase_Qty = model1.Purchase_Qty;
                            packingListByToolSize[i].Received_Qty = model1.Received_Qty;
                            packingListByToolSize[i].Act = model1.Act;
                            packingListByToolSize[i].Bal = model1.Bal;
                        }
                    }
                }
            }
            var count= packingListDetailModel.Count();
            if(count > 0 && count <=8) {
                packingListDetailModel1 = packingListDetailModel;
            } else if (count > 8 && count <= 16) {
                for (int i = 0; i < 8; i++)
                {
                    packingListDetailModel1.Add(packingListDetailModel[i]);
                }
                for (int i = 8; i < count; i++)
                {
                    packingListDetailModel2.Add(packingListDetailModel[i]);
                }
            } else if(count > 16) {
                for (int i = 0; i < 8; i++)
                {
                    packingListDetailModel1.Add(packingListDetailModel[i]);
                }
                for (int i = 8; i < 16; i++)
                {
                    packingListDetailModel2.Add(packingListDetailModel[i]);
                }
                for (int i = 16; i < count; i++)
                {
                    packingListDetailModel3.Add(packingListDetailModel[i]);
                }
            }
            // Lấy dữ liệu show phần Suggested Location Material Form
            var transactionMain = await _repoTransactionMain.GetAll().ToListAsync();
            var transactionMain1 = transactionMain.Where(x => x.QRCode_ID.Trim() == qrCodeId.Trim() &&
                    (x.Transac_Type == "I" || x.Transac_Type == "M" || x.Transac_Type == "R") &&
                    x.Can_Move.Trim() == "Y");
                    var suggestedReturn1 = transactionMain1.Select(x => new {
                        rack_Location = x.Rack_Location
                    }).Distinct().ToList();
             // Lấy dữ liệu show phần Suggested Location Sorting Form
            var transactionMain2 = transactionMain
                    .Where(x => x.QRCode_ID.Trim() == qrCodeId.Trim() &&
                    x.Can_Move.Trim() == "Y");
            var suggestedReturn2 = transactionMain2.Select(x => new {
                        rack_Location = x.Rack_Location
                    }).Distinct().ToList();
                var result = new {
                    totalPQty,
                    totalRQty,
                    packingListDetailModel1, 
                    packingListDetailModel2,
                    packingListDetailModel3,
                    suggestedReturn1,
                    suggestedReturn2,
                };
                return result;
        }

        // ---Phần in QrCodeId khi vào Menu 8.1 QrCode Print => Print (Sorting Form)------//
        public async Task<object> FindByQrCodeIDAgain(QrCodeIDVersion data)
        {
            var qrCodeMan = await _repoQrcode.GetAll()
                    .Where(x => x.QRCode_ID.Trim() == data.QRCode_ID.Trim() &&
                        x.QRCode_Version == data.QRCode_Version).FirstOrDefaultAsync();  
            var packingListDetailModel = new List<PackingListDetailViewModel>();
            var packingListDetailModel1 = new List<PackingListDetailViewModel>();
            var packingListDetailModel2 = new List<PackingListDetailViewModel>();
            var packingListDetailModel3 = new List<PackingListDetailViewModel>();
            decimal? totalPQty = 0;
            decimal? totalRQty = 0;
            var transaction = await _repoTransactionMain.GetAll().Where(x => x.QRCode_ID.Trim() == data.QRCode_ID.Trim() &&
                    x.QRCode_Version == data.QRCode_Version).FirstOrDefaultAsync();
            var transactionDetails = await _repoTransactionDetail.GetAll()
                .Where(x => x.Transac_No.Trim() == transaction.Transac_No.Trim()).ToListAsync();
            
            var lists = await _repoPackingListDetail.GetAll()
                .Where(x => x.Receive_No.Trim() == qrCodeMan.Receive_No.Trim()).ToListAsync();
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
                foreach (var item1 in transactionDetails)
                {
                    if (item1.Tool_Size.Trim() == item.Tool_Size.Trim() && item1.Order_Size.Trim() == item.Order_Size.Trim()) {
                        packingItem.Received_Qty = item1.Qty;
                        packingItem.Act = item1.Instock_Qty;
                        packingItem.Bal = item1.Untransac_Qty;
                        totalRQty = totalRQty + item1.Instock_Qty;
                    }
                }
                totalPQty = totalPQty + item.Purchase_Qty;
                packingListDetailModel.Add(packingItem);
            }

             //----------------- Xử lý mảng dữ liệu cho 1 số dòng cùng tool size.----------------//
               // List các Tool Size mà có nhiều Order Size trong bảng Packing List Detail
            var toolSizeMoreOrderSize = lists.Where(x => x.Tool_Size.Trim() != x.Order_Size.Trim()).Select(x => x.Tool_Size).Distinct().ToList();
            if(toolSizeMoreOrderSize.Count() > 0) {
                foreach (var itemToolSize in toolSizeMoreOrderSize)
                {
                    var model1 = packingListDetailModel.Where(x => x.Tool_Size.Trim() == itemToolSize.Trim())
                        .GroupBy(x => x.Tool_Size).Select(x => new {
                            Purchase_Qty = x.Sum(cl => cl.Purchase_Qty),
                            Received_Qty = x.Sum(cl => cl.Received_Qty),
                            Act = x.Sum(cl => cl.Act),
                            Bal = x.Sum(cl => cl.Bal)
                        }).FirstOrDefault();
                    var packingListByToolSize = packingListDetailModel
                        .Where(x => x.Tool_Size.Trim() == itemToolSize.Trim()).ToList();
                    for (var i = 0; i < packingListByToolSize.Count; i ++)
                    {
                        if(i != 0) {
                            packingListByToolSize[i].Purchase_Qty = null;
                            packingListByToolSize[i].Received_Qty = null;
                            packingListByToolSize[i].Act = null;
                            packingListByToolSize[i].Bal = null;
                        } else {
                            packingListByToolSize[i].Purchase_Qty = model1.Purchase_Qty;
                            packingListByToolSize[i].Received_Qty = model1.Received_Qty;
                            packingListByToolSize[i].Act = model1.Act;
                            packingListByToolSize[i].Bal = model1.Bal;
                        }
                    }
                }
            }
            var count= packingListDetailModel.Count();
            if(count > 0 && count <=8) {
                packingListDetailModel1 = packingListDetailModel;
            } else if (count > 8 && count <= 16) {
                for (int i = 0; i < 8; i++)
                {
                    packingListDetailModel1.Add(packingListDetailModel[i]);
                }
                for (int i = 8; i < count; i++)
                {
                    packingListDetailModel2.Add(packingListDetailModel[i]);
                }
            } else if(count > 16) {
                for (int i = 0; i < 8; i++)
                {
                    packingListDetailModel1.Add(packingListDetailModel[i]);
                }
                for (int i = 8; i < 16; i++)
                {
                    packingListDetailModel2.Add(packingListDetailModel[i]);
                }
                for (int i = 16; i < count; i++)
                {
                    packingListDetailModel3.Add(packingListDetailModel[i]);
                }
            }
            // Lấy dữ liệu show phần Suggested Location Material Form
            var transactionMain = await _repoTransactionMain.GetAll().ToListAsync();
            var transactionMain1 = transactionMain.Where(x => x.QRCode_ID.Trim() == data.QRCode_ID.Trim() &&
                    (x.Transac_Type == "I" || x.Transac_Type == "M" || x.Transac_Type == "R") &&
                    x.Can_Move.Trim() == "Y");
                    var suggestedReturn1 = transactionMain1.Select(x => new {
                        rack_Location = x.Rack_Location
                    }).Distinct().ToList();
             // Lấy dữ liệu show phần Suggested Location Sorting Form
            var transactionMain2 = transactionMain
                    .Where(x => x.QRCode_ID.Trim() == data.QRCode_ID.Trim() &&
                    x.Can_Move.Trim() == "Y");
            var suggestedReturn2 = transactionMain2.Select(x => new {
                        rack_Location = x.Rack_Location
                    }).Distinct().ToList();
                var result = new {
                    totalPQty,
                    totalRQty,
                    packingListDetailModel1, 
                    packingListDetailModel2,
                    packingListDetailModel3,
                    suggestedReturn1,
                    suggestedReturn2,
                };
                return result;
        }
        public async Task<List<object>> PrintByQRCodeIDList(List<QrCodeIDVersion> data)
        {
            var packingList =  _repoPackingList.GetAll();
            var listQrCodeMain = _repoQrcode.GetAll();
            var listQrCodeModel = from x in listQrCodeMain join y in packingList
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
                };
            var objectResult = new List<object>();
            foreach (var item in data)
            {   
                var qrCodeMainItem = await listQrCodeModel
                    .Where(x => x.QRCode_ID.Trim() == item.QRCode_ID.Trim()).FirstOrDefaultAsync();
                var object1 = await this.FindByQrCodeID(item);
                var objectItem = new {
                    object1,
                    qrCodeMainItem
                };
                objectResult.Add(objectItem);
            }
            return objectResult;
        }
        
        public async Task<List<object>> PrintByQRCodeID(List<string> listQrCode)
        {
            var packingList =  _repoPackingList.GetAll();
            var listQrCodeMain = _repoQrcode.GetAll();
            var listQrCodeModel = from x in listQrCodeMain join y in packingList
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
                };
            var objectResult = new List<object>();
            foreach (var item in listQrCode)
            {   
                var qrCodeMainItem = await listQrCodeModel
                    .Where(x => x.QRCode_ID.Trim() == item.Trim()).FirstOrDefaultAsync();
                var object1 = await this.FindByQrCode(item);
                var objectItem = new {
                    object1,
                    qrCodeMainItem
                };
                objectResult.Add(objectItem);
            }
            return objectResult;
        }
        public async Task<List<object>> PrintByQRCodeIDListAgain(List<QrCodeIDVersion> data)
        {
            var packingList =  _repoPackingList.GetAll();
            var listQrCodeMain = _repoQrcode.GetAll();
            var listQrCodeModel = from x in listQrCodeMain join y in packingList
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
                };
            var objectResult = new List<object>();
            foreach (var item in data)
            {   
                var qrCodeMainItem = await listQrCodeModel
                    .Where(x => x.QRCode_ID.Trim() == item.QRCode_ID.Trim()).FirstOrDefaultAsync();
                var object1 = await this.FindByQrCodeIDAgain(item);
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
            return await _repoPackingListDetail.GetAll().ProjectTo<Packing_List_Detail_Dto>(_configMapper).ToListAsync();
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