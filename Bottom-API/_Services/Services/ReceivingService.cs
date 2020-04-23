using AutoMapper;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using System.Threading.Tasks;
using Bottom_API.Helpers;
using Bottom_API._Repositories.Interfaces;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Collections.Generic;
using Bottom_API.ViewModel;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Bottom_API._Services.Services
{
    public class ReceivingService : IReceivingService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IMaterialPurchaseRepository _repoPurchase;
        private readonly IMaterialMissingRepository _repoMissing;
        private readonly IMaterialViewRepository _repoMaterialView;
        private readonly IPackingListRepository _repoPackingList;
        private readonly IPackingListDetailRepository _repoPackingListDetail;
        private readonly IPackingListService _packingListService;
        private readonly IPackingListDetailService _packingListDetailService;
        public ReceivingService(IMaterialPurchaseRepository repoPurchase,
                                IMaterialMissingRepository repoMissing,
                                IMaterialViewRepository repoMaterialView,
                                IPackingListRepository repoPackingList,
                                IPackingListDetailRepository repoPackingListDetail,
                                IPackingListService packingListService,
                                IPackingListDetailService packingListDetailService,
                                IMapper mapper,
                                MapperConfiguration configMapper)
        {
            _repoMissing = repoMissing;
            _repoPurchase = repoPurchase;
            _repoMaterialView = repoMaterialView;
            _repoPackingList = repoPackingList;
            _repoPackingListDetail = repoPackingListDetail;
            _packingListService = packingListService;
            _packingListDetailService = packingListDetailService;
            _configMapper = configMapper;
            _mapper = mapper;

        }

        public Task<bool> Add(Receiving_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public class PurchaseConvert {
            public string Purchase_No {get;set;}
            public string Status {get;set;}
            public string Missing_No {get;set;}
        }
        public async Task<object> MaterialMerging(MaterialMainViewModel model)
        {
            var listMaterial = new List<Material_Dto>();
            if (model.Missing_No != "") {
                listMaterial = await _repoMissing.GetAll().ProjectTo<Material_Dto>(_configMapper)
                .Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim()).ToListAsync();
            } else {
                listMaterial = await _repoPurchase.GetAll().ProjectTo<Material_Dto>(_configMapper)
                .Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim()).ToListAsync();
            }
            listMaterial.OrderByDescending(x => x.MO_Seq);
            var listBatch = listMaterial.GroupBy(x => x.MO_Seq).Select(y => new {
                MO_Seq = y.First().MO_Seq
            }).ToList();
            var list3 = new List<OrderSizeByBatch>();
            foreach (var item in listBatch)
            {
                var item1 = new OrderSizeByBatch();
                item1.MO_Seq = item.MO_Seq;
                item1.Purchase_No = model.Purchase_No;
                item1.Missing_No = model.Missing_No;
                item1.Material_ID = model.Material_ID;
                item1.Material_Name = model.Material_Name;
                item1.Model_No = model.Model_No;
                item1.Model_Name = model.Model_Name;
                item1.MO_No = model.MO_No;
                item1.Article = model.Article;
                item1.Supplier_ID = model.Supplier_ID;
                item1.Supplier_Name = model.Supplier_Name;
                item1.Subcon_No = model.Subcon_No;
                item1.Subcon_Name = model.Subcon_Name;
                item1.T3_Supplier = model.T3_Supplier;
                item1.T3_Supplier_Name = model.T3_Supplier_Name;
                item1.CheckInsert = "1";
                foreach (var item4 in listMaterial)
                {
                    if(item4.MO_Seq == item.MO_Seq) {
                        if (item4.Accumlated_In_Qty != item4.Purchase_Qty) {
                            item1.CheckInsert = "0";
                            break;
                        }
                    }
                }
                var item3 = new List<OrderSizeAccumlate>();
                foreach (var item2 in listMaterial)
                {
                    if (item2.MO_Seq == item.MO_Seq) {
                        var item4 = new OrderSizeAccumlate();
                        item4.Order_Size = item2.Order_Size;
                        item4.Model_Size = item2.Model_Size;
                        item4.Tool_Size = item2.Tool_Size;
                        item4.Spec_Size = item2.Spec_Size;
                        item4.Purchase_Qty_Const = item2.Purchase_Qty;
                        item4.MO_Qty = item2.MO_Qty;
                        item4.Purchase_Qty = item2.Purchase_Qty - item2.Accumlated_In_Qty;
                        item4.Accumlated_In_Qty = item2.Accumlated_In_Qty;
                        item4.Received_Qty = 0;
                        item3.Add(item4);
                    };
                    item1.Purchase_Qty = item3;
                }
                list3.Add(item1);
            }

            var list1 = listMaterial.GroupBy(l => l.Order_Size)
                .Select(cl => new  {
                    Order_Size = cl.First().Order_Size,
                    Accumlated_In_Qty = cl.Sum(c => c.Accumlated_In_Qty),
                    Delivery_Qty_Batches = cl.Sum(x => x.Accumlated_In_Qty),
                    Purchase_Qty = cl.Sum(c => c.Purchase_Qty),
                    Delivery_Qty = cl.Sum(c => c.Purchase_Qty) - cl.Sum(c => c.Accumlated_In_Qty)
                }).ToList();
            var list2 = listMaterial.GroupBy(x => new {x.Order_Size, x.Purchase_Qty, x.MO_Seq})
            .Select(y => new {
                Order_Size = y.First().Order_Size,
                Purchase_Qty = y.First().Purchase_Qty,
                MO_Seq = y.First().MO_Seq,
            });
            var list4 = new List<MaterialMergingViewMode>();
            foreach (var item in list1)
            {
                var arrayItem = new MaterialMergingViewMode();
                arrayItem.Order_Size = item.Order_Size;
                arrayItem.Purchase_Qty = item.Purchase_Qty;
                arrayItem.Accumlated_In_Qty = item.Accumlated_In_Qty;
                arrayItem.Delivery_Qty = item.Delivery_Qty;
                arrayItem.Delivery_Qty_Batches = item.Delivery_Qty_Batches;
                var array1 = new List<BatchQtyItem>();
                foreach (var item1 in list2)
                {
                    if(item1.Order_Size.Trim() == item.Order_Size.Trim()) {
                        var item2 = new BatchQtyItem();
                        item2.MO_Seq = item1.MO_Seq;
                        item2.Purchase_Qty = item1.Purchase_Qty;
                        array1.Add(item2);
                        arrayItem.Purchase_Qty_Item = array1;
                    }
                }
                list4.Add(arrayItem);
            }
            var result = new {
                list3,
                list4

            };
            return result;
        }
        public async Task<List<MaterialMainViewModel>> SearchByModel(FilterMaterialParam filterParam)
        {
            var listMaterialView = await _repoMaterialView.FindAll().ToListAsync();
            var materialPurchaseList = await _repoPurchase.FindAll()
            .Where(x => x.Confirm_Delivery >= Convert.ToDateTime(filterParam.From_Date + " 00:00:00.000") &&
                        x.Confirm_Delivery <= Convert.ToDateTime(filterParam.To_Date + " 23:59:59.997")).Select(x => new {
                            Purchase_No = x.Purchase_No,
                            Status = x.Status,
                            Missing_No = ""
                        }).Distinct().ToListAsync();

            var materialMissingList = await _repoMissing.GetAll()
            .Where(x => x.Confirm_Delivery >= Convert.ToDateTime(filterParam.From_Date + " 00:00:00.000") &&
                        x.Confirm_Delivery <= Convert.ToDateTime(filterParam.To_Date + " 23:59:59.997"))
                        .Select(x => new {
                            Purchase_No = x.Purchase_No,
                            Status = x.Status,
                            Missing_No = x.Missing_No
                        }).Distinct().ToListAsync();
            foreach (var item in materialMissingList)
            {
                materialPurchaseList.Add(item);
            }
            // Nếu purchase đó có 1 batch là N thì status show ra sẽ là N. Còn Y hết thì hiển thị Y
            // Tạo ra 1 mảng đối tượng mới
            var materialPurchaseListConvert = new List<PurchaseConvert>();
            foreach (var item in materialPurchaseList)
            {
                var item1 = new PurchaseConvert {
                    Purchase_No = item.Purchase_No,
                    Status = item.Status,
                    Missing_No = item.Missing_No
                };
                materialPurchaseListConvert.Add(item1);
            }
            foreach (var item in materialPurchaseListConvert)
            {
                if (item.Status.Trim() == "N") {
                    foreach (var item1 in materialPurchaseListConvert)
                    {
                        if(item1.Purchase_No.Trim() == item.Purchase_No.Trim() && item1.Missing_No == item.Missing_No)
                        item1.Status = "N";
                    }
                }
            }
            // Distinct lại mảng.Do ko xài Distinct ở trong câu lệnh 1 list Object được.
            var listData = materialPurchaseListConvert.GroupBy(x => new{ x.Purchase_No, x.Missing_No}).Select(y => y.First());
            // --------------------------------------------------------------------------
            var listMaterial = (from a in listData join b in listMaterialView
                                on a.Purchase_No.Trim() equals b.Purchase_No.Trim()
                                select new MaterialMainViewModel {
                                    Status = a.Status,
                                    Material_ID = b.Mat_,
                                    Material_Name = b.Mat__Name,
                                    Missing_No = a.Missing_No == null? "" : a.Missing_No,
                                    MO_No = b.Plan_No,
                                    Purchase_No = a.Purchase_No,
                                    Model_No = b.Model_No,
                                    Model_Name = b.Model_Name,
                                    Article = b.Article,
                                    Supplier_ID = b.Supplier_No,
                                    Supplier_Name = b.Supplier_Name,
                                    Subcon_No = b.Subcon_No,
                                    Subcon_Name = b.Subcon_Name,
                                    T3_Supplier = b.T3_Supplier,
                                    T3_Supplier_Name = b.T3_Supplier_Name
                                }).ToList();
            // var lists = new List<MaterialMainViewModel>();
            // lists.AddRange(materialPurchaseList);
            // lists.AddRange(materialMissingList);
            // var list2 =  Queryable.Union(test1.AsQueryable(), test2.AsQueryable());
            // var list4 = list2.AsQueryable();
            // var list3 = Queryable.Concat(materialPurchaseList.AsQueryable(),materialMissingList.AsQueryable());
            if(filterParam.MO_No != null && filterParam.MO_No != string.Empty) {
                listMaterial = listMaterial.Where(x => x.Purchase_No.Trim() == filterParam.MO_No.Trim()).ToList();
            }
            if(filterParam.Supplier_ID != null && filterParam.Supplier_ID != string.Empty) {
                listMaterial = listMaterial.Where(x => x.Supplier_ID.Trim() == filterParam.Supplier_ID.Trim()).ToList();
            }
            if (filterParam.Status != "all") {
                listMaterial = listMaterial.Where(x => x.Status.Trim() == filterParam.Status.Trim()).ToList();
            }
            return listMaterial;
        }
        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Receiving_Dto>> Filter(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Receiving_Dto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Receiving_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Receiving_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Receiving_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Receiving_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ReceiveNoMain>> UpdateMaterial(List<OrderSizeByBatch> data)
        {
            var Purchase_No = data[0].Purchase_No;
            // --------------------------------------------------------------------------------------------//
            if (data[0].Missing_No == "") {
                // --------Update lại Accumlated_In_Qty theo Purchase_No,Order_Size và Mo_Seq ở bảng Material_Purchase----//
                foreach (var item in data)
                {
                    foreach (var item1 in item.Purchase_Qty)
                    {
                        var materialItem = await _repoPurchase.FindAll()
                            .Where(x => x.Purchase_No.Trim() == Purchase_No.Trim() &&
                                    x.Order_Size == item1.Order_Size &&
                                    x.MO_Seq == item.MO_Seq).FirstOrDefaultAsync();
                        // materialItem.Accumlated_In_Qty = item1.Accumlated_In_Qty;
                        // Số lượng bằng số lượng nhận hiện tại + số lượng vừa mới nhận vào.
                        materialItem.Accumlated_In_Qty  = materialItem.Accumlated_In_Qty + item1.Received_Qty;
                        await _repoPurchase.SaveAll();
                    }
                }

                //------------------------- Update giá trị Status--------------------------------------------//
                foreach (var item in data)
                {
                    await this.UpdateStatusMaterial(item.Purchase_No, item.MO_Seq, item.Missing_No);
                }
                await _repoPurchase.SaveAll();
            } else {
                // Update lại Accumlated_In_Qty theo Purchase_No,Order_Size và Mo_Seq ở bảng Material_Missing
                foreach (var item in data)
                {
                    foreach (var item1 in item.Purchase_Qty)
                    {
                        var materialItem = await _repoMissing.FindAll()
                            .Where(x => x.Purchase_No.Trim() == Purchase_No.Trim() &&
                                    x.Order_Size == item1.Order_Size &&
                                    x.MO_Seq == item.MO_Seq).FirstOrDefaultAsync();
                        materialItem.Accumlated_In_Qty = item1.Accumlated_In_Qty;
                    }
                }

                // Update lại Status
                foreach (var item in data)
                {
                    await this.UpdateStatusMaterial(item.Purchase_No, item.MO_Seq, item.Missing_No);
                }

                await _repoMissing.SaveAll();
            }

               //------------------------Thêm vào 2 bảng Packing_List và Packing_List_Detail------------------//
            var ReceiveNoMain = new List<ReceiveNoMain>();
            foreach (var item in data)
            {
                // Check xem có tiến hành thêm hay ko
                var checkAdd = false;
                foreach (var item1 in item.Purchase_Qty)
                {
                // Kiểm tra nếu tồn tại Received_Qty lớn hơn 0,có nghĩa là tồn tại 1 Order_Size trong batch đó có nhận hàng
                    if (item1.Received_Qty > 0) {
                        checkAdd = true;
                        break;
                    }
                }
                    // Tiến hành thêm vào bảng Packing_List và Packing_List_Detail
                if (checkAdd == true) {
                    var packing_List = new Packing_List_Dto();
                    packing_List.Sheet_Type = "R";
                    packing_List.Missing_No = item.Missing_No;
                    packing_List.Supplier_ID = item.Supplier_ID;
                    packing_List.Supplier_Name = item.Supplier_Name;
                    packing_List.MO_No = item.MO_No;
                    packing_List.Purchase_No = item.Purchase_No;
                    packing_List.MO_Seq = item.MO_Seq;
                    packing_List.Delivery_No = item.Delivery_No;
                    packing_List.Material_ID = item.Material_ID;
                    packing_List.Material_Name = item.Material_Name;
                    packing_List.Model_No = item.Model_No;
                    packing_List.Model_Name = item.Model_Name;
                    packing_List.Article = item.Article;
                    packing_List.Subcon_ID = item.Subcon_No;
                    packing_List.Subcon_Name = item.Subcon_Name;
                    packing_List.T3_Supplier = item.T3_Supplier;
                    packing_List.T3_Supplier_Name = item.T3_Supplier_Name;
                    packing_List.Generated_QRCode = "N";
                    packing_List.Receive_Date = DateTime.Now;
                    packing_List.Updated_By = "Phi Long";
                    packing_List.Receive_No = this.RandomString(2);

                    // Tạo ra thông tin của 1 Receive No
                    var ReceiveNoItem = new ReceiveNoMain();
                    ReceiveNoItem.MO_No = item.MO_No;
                    ReceiveNoItem.Purchase_No = item.Purchase_No;
                    ReceiveNoItem.Receive_No = packing_List.Receive_No;
                    ReceiveNoItem.MO_Seq = item.MO_Seq;
                    ReceiveNoItem.Receive_Date = packing_List.Receive_Date;
                    ReceiveNoItem.Sheet_Type = packing_List.Sheet_Type;
                    ReceiveNoItem.Updated_By = packing_List.Updated_By;
                    ReceiveNoItem.Purchase_Qty = 0;
                    ReceiveNoItem.Accumated_Qty = 0;
                    await _packingListService.Add(packing_List);

                    foreach (var item2 in item.Purchase_Qty)
                    {
                        var packing_List_detail = new Packing_List_Detail_Dto();
                        packing_List_detail.Receive_No = packing_List.Receive_No;
                        packing_List_detail.Order_Size = item2.Order_Size;
                        packing_List_detail.Model_Size = item2.Model_Size;
                        packing_List_detail.Tool_Size = item2.Tool_Size;
                        packing_List_detail.Spec_Size = item2.Spec_Size;
                        packing_List_detail.MO_Qty = item2.MO_Qty;
                        packing_List_detail.Purchase_Qty = item2.Purchase_Qty_Const;
                        packing_List_detail.Received_Qty = item2.Received_Qty;
                        packing_List_detail.Updated_Time = DateTime.Now;
                        packing_List_detail.Updated_By = "Phi Long";

                        ReceiveNoItem.Purchase_Qty = ReceiveNoItem.Purchase_Qty + item2.Purchase_Qty_Const;
                        ReceiveNoItem.Accumated_Qty = ReceiveNoItem.Accumated_Qty + item2.Received_Qty;
                        await _packingListDetailService.Add(packing_List_detail);
                    }
                    ReceiveNoMain.Add(ReceiveNoItem);
                }
            }
            return ReceiveNoMain;
        }

        // Hàm lấy random string
        public string RandomString(int size)
        {   var datetimeNow = DateTime.Now;
            var year = datetimeNow.Year.ToString();
            var month = datetimeNow.Month;
            var day = datetimeNow.Day;
            var arrayYear = year.ToCharArray().Select(c => c.ToString()).ToArray();
            var yearString = arrayYear[2] + arrayYear[3];
            var monthString = "";
            var dayString = "";
            if (month >=10) {
                monthString = month.ToString();
            } else {
                monthString = "0" + month;
            }

            if (day >= 10) {
                dayString = day.ToString();
            } else {
                dayString = "0" + day;
            }
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            var stringResult = "RW" + yearString + monthString + dayString + builder.ToString().ToUpper();
            return stringResult;
        }


        // ------------------Hàm lấy chi tiết của 1 Receive_No.------------------------------------------------------------
        public async Task<List<ReceiveNoDetail>> ReceiveNoDetails(string receive_No)
        {
            var packingList = await _repoPackingList.GetAll().Where(x => x.Receive_No.Trim() == receive_No.Trim()).FirstOrDefaultAsync();
            var materialList = new List<Material_Dto>();
            if(packingList.Missing_No == "") {
                materialList = await _repoPurchase.GetAll()
                    .Where(x => x.Purchase_No.Trim() == packingList.Purchase_No.Trim() &&
                            x.MO_Seq.Trim() == packingList.MO_Seq.Trim())
                    .ProjectTo<Material_Dto>(_configMapper).ToListAsync();
            } else {
                await _repoMissing.GetAll()
                    .Where(x => x.Purchase_No.Trim() == packingList.Purchase_No.Trim() &&
                            x.MO_Seq.Trim() == packingList.MO_Seq.Trim())
                    .ProjectTo<Material_Dto>(_configMapper).ToListAsync();
            }
            var listData = materialList.Select( x => new ReceiveNoDetail() {
                Order_Size = x.Order_Size,
                Purchase_Qty = x.Purchase_Qty,
                Received_Qty = x.Accumlated_In_Qty,
                Remaining = x.Purchase_Qty - x.Accumlated_In_Qty
            }).ToList();
            return listData;
        }

        public async Task<List<ReceiveNoMain>> ReceiveNoMain(MaterialMainViewModel model)
        {
            var packingList = await _packingListService.GetAllAsync();
            var packingListDetail = await _packingListDetailService.GetAllAsync();
            var data = (from a in packingList
                        join b in packingListDetail on a.Receive_No.Trim() equals b.Receive_No.Trim()
                        where a.Purchase_No.Trim() == model.Purchase_No.Trim()
                        select new {
                            Status = model.Status,
                            Purchase_No = model.Purchase_No,
                            Delivery_No = a.Delivery_No,
                            Missing_No = model.Missing_No,
                            MO_No = model.MO_No,
                            Receive_No = a.Receive_No,
                            MO_Seq = a.MO_Seq,
                            Receive_Date = a.Receive_Date,
                            Purchase_Qty = b.Purchase_Qty,
                            Received_Qty = b.Received_Qty,
                            Generated_QRCode = a.Generated_QRCode,
                            Sheet_Type = a.Sheet_Type,
                            Updated_By = a.Updated_By
                        }).GroupBy(x => x.Receive_No).Select( cl => new ReceiveNoMain() {
                            MO_No = cl.First().MO_No,
                            Missing_No = cl.First().Missing_No,
                            Purchase_No = cl.First().Purchase_No,
                            Delivery_No = cl.First().Delivery_No,
                            Receive_No = cl.First().Receive_No,
                            MO_Seq = cl.First().MO_Seq,
                            Receive_Date = cl.First().Receive_Date,
                            Purchase_Qty = cl.Sum(c => c.Purchase_Qty),
                            Accumated_Qty = cl.Sum(c => c.Received_Qty),
                            Generated_QRCode = cl.First().Generated_QRCode,
                            Sheet_Type = cl.First().Sheet_Type,
                            Updated_By = cl.First().Updated_By
                        }).OrderByDescending(x => x.Receive_Date).ToList();
            return data;
        }

        public async Task<bool> ClosePurchase(MaterialMainViewModel model)
        {
            if (model.Missing_No == "") {
                var purchaseList = await _repoPurchase.GetAll()
                    .Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim()).ToListAsync();
                foreach (var item in purchaseList)
                {
                    item.Status = "Y";
                }
                return await _repoPurchase.SaveAll();
            } else {
                var purchaseList = await _repoMissing.GetAll()
                    .Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim()).ToListAsync();
                foreach (var item in purchaseList)
                {
                    item.Status = "Y";
                }
                return await _repoMissing.SaveAll();
            }
        }

        public async Task<string> StatusPurchase(MaterialMainViewModel model)
        {
            var packingList = _repoPackingList.GetAll().Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim());
            var material = new List<Material_Dto>();
            var status = "ok";
            if(model.Missing_No == "") {
                material = await _repoPurchase.GetAll().Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim())
                    .ProjectTo<Material_Dto>(_configMapper).ToListAsync();
            } else {
                material = await _repoMissing.GetAll().Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim())
                    .ProjectTo<Material_Dto>(_configMapper).ToListAsync();
            }

            // Nếu purchase đó chưa có hàng nhận vào.
            if (packingList.Count() > 0) {
                foreach (var item in packingList)
                {
                    // Nếu trong bảng packingList có 1 receiveNo của purchase đó chưa đc sản sinh qrcode thì 
                    // chưa cho thêm hàng tiếp
                    if(item.Generated_QRCode.Trim() == "N") {
                        status = "no";
                        break;
                    }
                }
            } else {
                status = "ok";
            }

            // Nếu tồn tại 1 Status trong bảng Purchase hoặc bảng Missing có status = N thì thêm đc.Còn Y là
            // đã đủ hàng rồi.Ko đc thêm tiếp.
            if(status == "ok") {
                var checkmaterial = "enough";
                foreach (var item in material)
                {
                    if (item.Status == "N") {
                        checkmaterial = "not enough";
                        break;
                    }
                }
                if(checkmaterial == "enough") {
                    status = "no";
                }
            }
            return status;
        }

        public async Task<List<MaterialEditModel>> EditMaterial(ReceiveNoMain model)
        {
            var materialList = new List<Material_Dto>();
            if(model.Missing_No == "") {
                materialList = await _repoPurchase.GetAll()
                .Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim() &&
                        x.MO_Seq.Trim() == model.MO_Seq.Trim())
                    .ProjectTo<Material_Dto>(_configMapper).ToListAsync();
            } else {
                materialList = await _repoMissing.GetAll()
                .Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim() &&
                        x.MO_Seq.Trim() == model.MO_Seq.Trim())
                    .ProjectTo<Material_Dto>(_configMapper).ToListAsync();
            }
            var packingListDetail = await _repoPackingListDetail.GetAll()
                .Where(x => x.Receive_No.Trim() == model.Receive_No.Trim()).ToListAsync();
            var dataList1 = materialList.GroupBy(x => x.Order_Size).Select( y => new MaterialEditModel(){
                Purchase_No = y.First().Purchase_No,
                Missing_No = model.Missing_No,
                Receive_No = model.Receive_No,
                Order_Size = y.First().Order_Size,
                Purchase_Qty = y.Sum(cl => cl.Purchase_Qty),
                Accumated_Qty = y.Sum(cl => cl.Accumlated_In_Qty),
                Delivery_Qty = y.Sum(cl => (cl.Purchase_Qty - cl.Accumlated_In_Qty)),
                MO_Seq_Edit = model.MO_Seq,
            }).ToList();
            foreach (var item in dataList1)
            {
                foreach (var item1 in packingListDetail)
                {
                    if(item1.Order_Size.Trim() == item.Order_Size.Trim()) {
                        item.Received_Qty = item1.Received_Qty;
                        item.Received_Qty_Edit = item1.Received_Qty;
                    }
                }
            }
            return dataList1;
        }

        public async Task<bool> EditDetail(List<MaterialEditModel> data)
        {
            var editResult = false;
            var receive_No = data[0].Receive_No;
            var missing_No = data[0].Missing_No;
            var packinglistDetail = _repoPackingListDetail.GetAll().Where(x => x.Receive_No.Trim() == receive_No.Trim());
            foreach (var item in packinglistDetail)
            {
                foreach (var item1 in data)
                {
                    if (item1.Order_Size.Trim() == item.Order_Size.Trim()) {
                        item.Received_Qty = item1.Received_Qty_Edit;
                    }
                }
            }
            var SavePackingListDetail =  await _repoPackingListDetail.SaveAll();
            var saveMaterial = false;
            // Áp dụng cho bảng Material_Missing
            if(missing_No == "") {
                var materialPurchaseList = _repoPurchase.GetAll()
                    .Where(x => x.Purchase_No.Trim() == data[0].Purchase_No.Trim() &&
                            x.MO_Seq.Trim() == data[0].MO_Seq_Edit.Trim());
                foreach (var item2 in data)
                {
                    foreach (var item4 in materialPurchaseList)
                    {
                        if(item4.Order_Size.Trim() == item2.Order_Size.Trim()) {
                            // Số lượng mới = số lượng hiện tại trừ đi số lượng đã nhận trước và + cho số lượng nhận mới.
                            item4.Accumlated_In_Qty = item4.Accumlated_In_Qty - item2.Received_Qty + item2.Received_Qty_Edit;
                        }
                    }
                }
                await _repoPurchase.SaveAll();
                //------------------------- Update giá trị Status--------------------------------------------//
                await this.UpdateStatusMaterial(data[0].Purchase_No, data[0].MO_Seq_Edit, missing_No);
                saveMaterial = await _repoPurchase.SaveAll();
            } 
            // Áp dụng cho bảng Material_Missing
            else {
                var materialPurchaseList = _repoMissing.GetAll()
                    .Where(x => x.Purchase_No.Trim() == data[0].Purchase_No.Trim() &&
                            x.MO_Seq.Trim() == data[0].MO_Seq_Edit.Trim());
                foreach (var item2 in data)
                {
                    foreach (var item4 in materialPurchaseList)
                    {
                        if(item4.Order_Size.Trim() == item2.Order_Size.Trim()) {
                            // Số lượng mới = số lượng hiện tại trừ đi số lượng đã nhận trước và + cho số lượng nhận mới.
                            item4.Accumlated_In_Qty = item4.Accumlated_In_Qty - item2.Received_Qty + item2.Received_Qty_Edit;
                        }
                    }
                }
                await _repoMissing.SaveAll();
                await this.UpdateStatusMaterial(data[0].Purchase_No, data[0].MO_Seq_Edit, missing_No);
                saveMaterial = await _repoMissing.SaveAll();
            }
            if (SavePackingListDetail == true && saveMaterial == true) {
                editResult = true;
            }
            return editResult;
        }

        public async Task<bool> UpdateStatusMaterial(string purchaseNo, string mOSeq, string missingNo)
        {
            if (missingNo == "") {
                var checkStatus = "Y";
                    var purchaseForBatch = await _repoPurchase.FindAll()
                                    .Where(x => x.Purchase_No.Trim() == purchaseNo.Trim() &&
                                    x.MO_Seq == mOSeq).ToListAsync();
                foreach (var item1 in purchaseForBatch)
                    {
                        // Kiểm tra thì cần 1 dòng mà Accumlated_In_Qty khác Purchase_Qty thì checkStatus = N
                        // Và thoát khỏi vòng lặp hiện tại
                        if (item1.Accumlated_In_Qty != item1.Purchase_Qty) {
                            checkStatus = "N";
                            break;
                        }
                    }
                    // Nếu mà Accumlated_In_Qty đều bằng Purchase_Qty có nghĩa là batch đó đã nhận đủ hàng.
                    // Cập nhập lại Status trong table của batch đó là Y.
                    if (checkStatus == "Y") {
                        foreach (var item3 in purchaseForBatch)
                        {
                            item3.Status = "Y";
                        }
                    } else {
                        foreach (var item3 in purchaseForBatch)
                        {
                            item3.Status = "N";
                        }
                    }
                return await _repoPurchase.SaveAll();
            } else {
                var checkStatus = "Y";
                var purchaseForBatch = await _repoMissing.FindAll()
                                    .Where(x => x.Purchase_No.Trim() == purchaseNo.Trim() &&
                                    x.MO_Seq == mOSeq).ToListAsync();
                foreach (var item1 in purchaseForBatch)
                    {
                        if (item1.Accumlated_In_Qty != item1.Purchase_Qty) {
                            checkStatus = "N";
                            break;
                        }
                    }
                    if (checkStatus == "Y") {
                        foreach (var item3 in purchaseForBatch)
                        {
                            item3.Status = "Y";
                        }
                    } else {
                         foreach (var item3 in purchaseForBatch)
                        {
                            item3.Status = "N";
                        }
                    }
                return await _repoPurchase.SaveAll();
            }
        }
    }
}