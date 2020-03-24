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
        public async Task<List<WMSB_Packing_ListDto>> GetAllAsync()
        {
            return await _repo.GetAll().ProjectTo<WMSB_Packing_ListDto>(_configMapper).ToListAsync();
        }

        public WMSB_Packing_ListDto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<WMSB_Packing_ListDto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<WMSB_Packing_ListDto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public async Task<WMSB_Packing_ListDto> FindBySupplier(string supplier_ID)
        {
            var data =  await _repo.GetAll()
                        .Where(x => x.Supplier_ID.Trim() == supplier_ID.Trim())
                        .FirstOrDefaultAsync();
            var model = _mapper.Map<WMSB_Packing_ListDto>(data);
            return model;
        }

        public async Task<PagedList<WMSB_Packing_ListDto>> SearchViewModel(PaginationParams param,PackingListSearchViewModel model)
        {
            var packingSearch =  _repo.GetAll().ProjectTo<WMSB_Packing_ListDto>(_configMapper).
                            Where(  x => x.Receive_Date >= Convert.ToDateTime(model.From_Date + " 00:00") &&
                                    x.Receive_Date <= Convert.ToDateTime(model.To_Date + " 00:00") &&
                                    x.Supplier_ID.Trim() == model.Supplier_ID.Trim() &&
                                    x.MO_No.Trim() == model.MO_No.Trim());
            return await PagedList<WMSB_Packing_ListDto>.CreateAsync(packingSearch, param.PageNumber, param.PageSize);
        }
    }
}