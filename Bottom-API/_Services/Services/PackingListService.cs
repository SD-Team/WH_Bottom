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
using Bottom_API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class PackingListService : IPackingListService
    {
        private readonly IPackingListRepository _repo;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public PackingListService(  IPackingListRepository repo,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repo = repo;
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

        public async Task<Packing_List_Dto> FindBySupplier(string supplier_ID)
        {
            var data =  await _repo.GetAll()
                        .Where(x => x.Supplier_ID.Trim() == supplier_ID.Trim())
                        .FirstOrDefaultAsync();
            var model = _mapper.Map<Packing_List_Dto>(data);
            return model;
        }

        public async Task<PagedList<Packing_List_Dto>> SearchViewModel(PaginationParams param,PackingListSearchViewModel model)
        {
            var packingSearch =  _repo.GetAll().ProjectTo<Packing_List_Dto>(_configMapper).
                            Where(  x => x.Receive_Date >= Convert.ToDateTime(model.From_Date + " 00:00") &&
                                    x.Generated_QRCode.Trim() == "N" &&
                                    x.Receive_Date <= Convert.ToDateTime(model.To_Date + " 00:00"));
            if (model.Supplier_ID != null) {
                packingSearch = packingSearch.Where(x => x.Supplier_ID.Trim() == model.Supplier_ID.Trim());
            }
            if (model.MO_No != null) {
                packingSearch = packingSearch.Where(x => x.MO_No.Trim() == model.MO_No.Trim());
            }
            return await PagedList<Packing_List_Dto>.CreateAsync(packingSearch, param.PageNumber, param.PageSize);
        }

        public Task<bool> Add(Packing_List_Dto model)
        {
            throw new NotImplementedException();
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