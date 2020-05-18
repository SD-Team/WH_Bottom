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
        private readonly IPackingListRepository _repo;
        private readonly IMaterialViewRepository _repoMaterialView;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public PackingListService(  IPackingListRepository repo,
                                    IMaterialViewRepository repoMaterialView,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repo = repo;
            _repoMaterialView = repoMaterialView;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<List<Packing_List_Dto>> GetAllAsync()
        {
            return await _repo.GetAll().ProjectTo<Packing_List_Dto>(_configMapper).ToListAsync();
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
                Supplier_No = x.Supplier_No,
                Supplier_Name = x.Supplier_Name
            }).Distinct().ToListAsync();
            return data;
        }

        public async Task<PagedList<Packing_List_Dto>> SearchViewModel(PaginationParams param,FilterPackingListParam filterParam)
        {
            var test = DateTime.Now.Date.ToString();
            var packingSearch =  _repo.GetAll().ProjectTo<Packing_List_Dto>(_configMapper).
                            Where(  x => x.Receive_Date >= DateTime.Parse(filterParam.From_Date + " 00:00:00.000") &&
                                    x.Generated_QRCode.Trim() == "N" &&
                                    x.Receive_Date <= DateTime.Parse(filterParam.To_Date + " 23:59:59.000")
                                    );
            if (filterParam.Supplier_ID != null && filterParam.Supplier_ID != string.Empty) {
                packingSearch = packingSearch.Where(x => x.Supplier_ID.Trim() == filterParam.Supplier_ID.Trim());
            }
            if (filterParam.MO_No != null && filterParam.MO_No != string.Empty) {
                packingSearch = packingSearch.Where(x => x.MO_No.Trim() == filterParam.MO_No.Trim());
            }
            return await PagedList<Packing_List_Dto>.CreateAsync(packingSearch, param.PageNumber, param.PageSize);
        }

        public async Task<bool> Add(Packing_List_Dto model)
        {
            var data = _mapper.Map<WMSB_Packing_List>(model);
            _repo.Add(data);
            return await _repo.SaveAll();
        }

        public Task<bool> Update(Packing_List_Dto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }
    }
}