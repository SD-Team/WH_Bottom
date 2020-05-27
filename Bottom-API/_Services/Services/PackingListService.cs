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
    public class PackingListService : IPackingListService
    {
        private readonly IPackingListRepository _repoPackingList;
        private readonly IMaterialViewRepository _repoMaterialView;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public PackingListService(  IPackingListRepository repoPackingList,
                                    IMaterialViewRepository repoMaterialView,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repoPackingList = repoPackingList;
            _repoMaterialView = repoMaterialView;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<List<Packing_List_Dto>> GetAllAsync()
        {
            return await _repoPackingList.GetAll().ProjectTo<Packing_List_Dto>(_configMapper).ToListAsync();
        }

        public Packing_List_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Packing_List_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Packing_List_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<SupplierModel>> SupplierList()
        {
            var data =  await _repoMaterialView.GetAll().Select(x => new SupplierModel {
                Supplier_No = x.Supplier_No.Trim(),
                Supplier_Name = x.Supplier_Name.Trim()
            }).Distinct().ToListAsync();
            return data;
        }

        public async Task<PagedList<WMSB_Packing_List>> SearchViewModel(PaginationParams param,FilterPackingListParam filterParam)
        {
            var packingSearch =  _repoPackingList.GetAll();
            if(filterParam.From_Date != null && filterParam.To_Date != null) {
                packingSearch =  packingSearch.
                            Where(  x => x.Receive_Date >= DateTime.Parse(filterParam.From_Date + " 00:00:00.000") &&
                                    x.Generated_QRCode.Trim() == "N" &&
                                    x.Receive_Date <= DateTime.Parse(filterParam.To_Date + " 23:59:59.000")
                                    );
            }
            if (filterParam.MO_No != null && filterParam.MO_No != string.Empty) {
                packingSearch = packingSearch.Where(x => x.MO_No.Trim() == filterParam.MO_No.Trim());
            }
            return await PagedList<WMSB_Packing_List>.CreateAsync(packingSearch, param.PageNumber, param.PageSize);
        }

        public async Task<Packing_List_Dto> FindBySupplier(string supplier_ID)
        {
            var data =  await _repoPackingList.GetAll()
                        .Where(x => x.Supplier_ID.Trim() == supplier_ID.Trim())
                        .FirstOrDefaultAsync();
            var model = _mapper.Map<Packing_List_Dto>(data);
            return model;
        }
        public async Task<bool> Add(Packing_List_Dto model)
        {
            var data = _mapper.Map<WMSB_Packing_List>(model);
            _repoPackingList.Add(data);
            return await _repoPackingList.SaveAll();
        }

        public Task<bool> Update(Packing_List_Dto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WMSB_Packing_List>> SearchNotPagination(FilterPackingListParam filterParam)
        {
            var packingSearch =  _repoPackingList.GetAll();
            if(filterParam.From_Date != null && filterParam.To_Date != null) {
                packingSearch =  packingSearch.
                            Where(  x => x.Receive_Date >= DateTime.Parse(filterParam.From_Date + " 00:00:00.000") &&
                                    x.Generated_QRCode.Trim() == "N" &&
                                    x.Receive_Date <= DateTime.Parse(filterParam.To_Date + " 23:59:59.000")
                                    );
            }
            if (filterParam.MO_No != null && filterParam.MO_No != string.Empty) {
                packingSearch = packingSearch.Where(x => x.MO_No.Trim() == filterParam.MO_No.Trim());
            }
            return await packingSearch.ToListAsync();
        }
    }
}