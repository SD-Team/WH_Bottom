using AutoMapper;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using System.Threading.Tasks;
using Bottom_API.DTO;
using System.Collections.Generic;
using Bottom_API.Helpers;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Bottom_API.ViewModel;
using System.Linq;
using System;

namespace Bottom_API._Services.Services
{
    public class MaterialPurchaseService : IMaterialPurchaseService
    {
        private readonly IMaterialPurchaseRepository _repoPurchase;
        private readonly IMaterialMissingRepository _repoMissing;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public MaterialPurchaseService( IMaterialPurchaseRepository repoPurchase,
                                        IMaterialMissingRepository repoMissing,
                                        IMapper mapper,
                                        MapperConfiguration configMapper) {
            _repoPurchase = repoPurchase;
            _repoMissing = repoMissing;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public Task<bool> Add(Material_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Material_Dto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Material_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }
        // public class Model1 {
        //     public string Order_Size {get;set;}
        //     public decimal? Purchase_Qty {get;set;}
        // }
        public Task<PagedList<Material_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> MaterialMerging(string Purchase_No)
        {
            var materialPurchase = await _repoPurchase.GetAll().ProjectTo<Material_Dto>(_configMapper)
                .Where(x => x.Purchase_No.Trim() == Purchase_No.Trim()).ToListAsync();
            var materialMissing = await _repoMissing.GetAll().ProjectTo<Material_Dto>(_configMapper)
                .Where(x => x.Purchase_No.Trim() == Purchase_No.Trim()).ToListAsync();
            var listMaterial = new List<Material_Dto>();
            listMaterial.AddRange(materialPurchase);
            listMaterial.AddRange(materialMissing);
            var list1 = listMaterial.GroupBy(l => l.Order_Size)
                .Select(cl => new  {
                    Order_Size = cl.First().Order_Size,
                    Purchase_Qty = cl.Sum(c => c.Purchase_Qty)
                }).ToList();
            var list2 = listMaterial.GroupBy(x => new {x.Order_Size, x.Purchase_Qty, x.MO_Seq})
            .Select(y => new {
                Order_Size = y.First().Order_Size,
                Purchase_Qty = y.First().Purchase_Qty,
                MO_Seq = y.First().MO_Seq,
            });
            var list3 = new List<MaterialMergingViewMode>();
            foreach (var item in list1)
            {
                var arrayItem = new MaterialMergingViewMode();
                arrayItem.Order_Size = item.Order_Size;
                arrayItem.Purchase_Qty = item.Purchase_Qty;
                var array1 = new List<BatchQtyItem>();
                foreach (var item1 in list2)
                {
                    if(item1.Order_Size.Trim() == item.Order_Size.Trim()) {
                        var item2 = new BatchQtyItem();
                        item2.MO_Seq = item1.MO_Seq;
                        item2.Purchase_Qty = item1.Purchase_Qty;
                        array1.Add(item2);
                        arrayItem.Purchase_Qty_Item = array1;
                        list3.Add(arrayItem);
                    }
                }
            }
            var result = new {
                list3
            };
            return result;
        }

        public Task<PagedList<Material_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<MaterialMainViewModel>> SearchByModel(MaterialSearchViewModel model)
        {
            var materialPurchaseList = await _repoPurchase.GetAll().ProjectTo<Material_Dto>(_configMapper)
            .Where(x => x.Confirm_Delivery >= Convert.ToDateTime(model.From_Date + " 00:00") &&
                        x.Confirm_Delivery <= Convert.ToDateTime(model.To_Date + " 00:00"))
            .Select(x => new MaterialMainViewModel() {
                Status = x.Status,
                Material_ID = x.Material_ID,
                Missing_No = "",
                MO_No = x.MO_No,
                Purchase_No = x.Purchase_No,
                Model_No = "",
                Model_Name = "",
                Article = "",
                Material_Name = "",
                Supplier_ID = x.Supplier_ID,
                Supplier_Name = "",
            }).Distinct().ToListAsync();
            var materialMissingList = await _repoMissing.GetAll().ProjectTo<Material_Dto>(_configMapper)
            .Where(x => x.Confirm_Delivery >= Convert.ToDateTime(model.From_Date + " 00:00") &&
                        x.Confirm_Delivery <= Convert.ToDateTime(model.To_Date + " 00:00"))
            .Select(x => new MaterialMainViewModel() {
                Status = x.Status,
                Material_ID = x.Material_ID,
                Material_Name = x.Material_Name,
                Missing_No = x.Missing_No,
                MO_No = x.MO_No,
                Purchase_No = x.Purchase_No,
                Model_No = x.Model_No,
                Model_Name = x.Model_Name,
                Article = x.Article,
                Supplier_ID = x.Supplier_ID,
                Supplier_Name = x.Supplier_Name,
            }).Distinct().ToListAsync();
            var lists = new List<MaterialMainViewModel>();
            lists.AddRange(materialPurchaseList);
            lists.AddRange(materialMissingList);
            // var list2 =  Queryable.Union(test1.AsQueryable(), test2.AsQueryable());
            // var list4 = list2.AsQueryable();
            // var list3 = Queryable.Concat(materialPurchaseList.AsQueryable(),materialMissingList.AsQueryable());
            if(model.Purchase_No != null && model.Purchase_No != "") {
                lists = lists.Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim()).ToList();
            }
            if(model.Supplier_ID != null && model.Supplier_ID != "") {
                lists = lists.Where(x => x.Supplier_ID.Trim() == model.Supplier_ID.Trim()).ToList();
            }
            if (model.Status != "all") {
                lists = lists.Where(x => x.Status.Trim() == model.Status.Trim()).ToList();
            }
            return lists;
            // return await PagedList<MaterialMainViewModel>.CreateAsync(list2, param.PageNumber, param.PageSize);
        }

        public Task<bool> Update(Material_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}