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

namespace Bottom_API._Services.Services
{
    public class ReceivingService : IReceivingService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IMaterialPurchaseRepository _repoPurchase;
        private readonly IMaterialMissingRepository _repoMissing;
        private readonly IMaterialViewRepository _repoMaterialView;
        public ReceivingService(IMaterialPurchaseRepository repoPurchase,
                                IMaterialMissingRepository repoMissing,
                                IMaterialViewRepository repoMaterialView,
                                IMapper mapper, 
                                MapperConfiguration configMapper)
        {
            _repoMissing = repoMissing;
            _repoPurchase = repoPurchase;
            _repoMaterialView = repoMaterialView;
            _configMapper = configMapper;
            _mapper = mapper;

        }

        public Task<bool> Add(Receiving_Dto model)
        {
            throw new System.NotImplementedException();
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
                var item3 = new List<OrderSizeAccumlate>();
                foreach (var item2 in listMaterial)
                {
                    if (item2.MO_Seq == item.MO_Seq) {
                        var item4 = new OrderSizeAccumlate();
                        item4.Purchase_Qty = item2.Purchase_Qty;
                        item4.Accumlated_In_Qty = item2.Accumlated_In_Qty;
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
        public async Task<List<MaterialMainViewModel>> SearchByModel(MaterialSearchViewModel model)
        {
            var listMaterialView = await _repoMaterialView.GetAll().ProjectTo<Material_View_Dto>(_configMapper).ToListAsync();
            var materialPurchaseList = await _repoPurchase.GetAll().ProjectTo<Material_Dto>(_configMapper)
            .Where(x => x.Confirm_Delivery >= Convert.ToDateTime(model.From_Date + " 00:00") &&
                        x.Confirm_Delivery <= Convert.ToDateTime(model.To_Date + " 00:00")).Select(x => new {
                            Purchase_No = x.Purchase_No,
                            Status = x.Status,
                            Missing_No = ""
                        }).Distinct().ToListAsync();
            var materialMissingList = await _repoMissing.GetAll().ProjectTo<Material_Dto>(_configMapper)
            .Where(x => x.Confirm_Delivery >= Convert.ToDateTime(model.From_Date + " 00:00") &&
                        x.Confirm_Delivery <= Convert.ToDateTime(model.To_Date + " 00:00"))
                        .Select(x => new {
                            Purchase_No = x.Purchase_No,
                            Status = x.Status,
                            Missing_No = x.Missing_No
                        }).Distinct().ToListAsync();
            foreach (var item in materialMissingList)
            {
                materialPurchaseList.Add(item);
            }
            var listMaterial = (from a in materialPurchaseList join b in listMaterialView
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
                                    Supplier_Name = b.Supplier_Name
                                }).ToList();
            // var lists = new List<MaterialMainViewModel>();
            // lists.AddRange(materialPurchaseList);
            // lists.AddRange(materialMissingList);
            // var list2 =  Queryable.Union(test1.AsQueryable(), test2.AsQueryable());
            // var list4 = list2.AsQueryable();
            // var list3 = Queryable.Concat(materialPurchaseList.AsQueryable(),materialMissingList.AsQueryable());
            if(model.Purchase_No != null && model.Purchase_No != "") {
                listMaterial = listMaterial.Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim()).ToList();
            }
            if(model.Supplier_ID != null && model.Supplier_ID != "") {
                listMaterial = listMaterial.Where(x => x.Supplier_ID.Trim() == model.Supplier_ID.Trim()).ToList();
            }
            if (model.Status != "all") {
                listMaterial = listMaterial.Where(x => x.Status.Trim() == model.Status.Trim()).ToList();
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
    }
}