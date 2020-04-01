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

        public Task<PagedList<Material_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Material_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Material_Dto>> SearchByModel(PaginationParams param, MaterialSearchViewModel model)
        {
            // var materialPurchaseList =  _repoPurchase.GetAll().ProjectTo<Material_Dto>(_configMapper)
            // .Select(x => new MaterialMainViewModel() {
            //     Status = x.Status,
            //     Material_ID = x.Material_ID,
            //     Missing_No = x.Missing_No,
            //     MO_No = x.MO_No,
            //     Purchase_No = x.Purchase_No,
            //     Model_No = x.Model_No,
            //     Model_Name = x.Model_Name,
            //     Article = x.Article,
            //     Supplier_ID = x.Supplier_ID,
            //     Supplier_Name = x.Supplier_Name
            // }).Distinct();
            // var materialMissingList =  _repoMissing.GetAll().ProjectTo<Material_Dto>(_configMapper)
            // .Select(x => new MaterialMainViewModel() {
            //     Status = x.Status,
            //     Material_ID = x.Material_ID,
            //     Missing_No = x.Missing_No,
            //     MO_No = x.MO_No,
            //     Purchase_No = x.Purchase_No,
            //     Model_No = x.Model_No,
            //     Model_Name = x.Model_Name,
            //     Article = x.Article,
            //     Supplier_ID = x.Supplier_ID,
            //     Supplier_Name = x.Supplier_Name
            // }).Distinct();
            // foreach (var item in materialMissingList)
            // {
            //     materialPurchaseList.Append(item);
            // }
            // foreach (var item in materialMissingList)
            // {
            //     materialPurchaseList = materialPurchaseList.Concat(item);
            // }
            // lists =  lists
            //     .Where(x => x.Confirm_Delivery >= Convert.ToDateTime(model.From_Date + " 00:00") &&
            //             x.Confirm_Delivery <= Convert.ToDateTime(model.To_Date + " 00:00"));
            // if(model.Purchase_No != null && model.Purchase_No != "") {
            //     lists = lists.Where(x => x.Purchase_No.Trim() == model.Purchase_No.Trim());
            // }
            // if(model.Supplier_ID != null && model.Supplier_ID != "") {
            //     lists = lists.Where(x => x.Supplier_ID.Trim() == model.Supplier_ID.Trim());
            // }
            // if (model.Status != "all") {
            //     lists = lists.Where(x => x.Status.Trim() == model.Status.Trim());
            // }
            // lists.OrderBy(x => x.Confirm_Delivery);
            // return await PagedList<Material_Dto>.CreateAsync(lists, param.PageNumber, param.PageSize);
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Material_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}