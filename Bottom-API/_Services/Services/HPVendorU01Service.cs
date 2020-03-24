using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class HPVendorU01Service : IHPVendorU01Service
    {
        private readonly IHPVendorU01Repository _repo;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public HPVendorU01Service(  IHPVendorU01Repository repo,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repo = repo;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<List<HPVendorU01Dto>> GetAllAsync()
        {
            return await _repo.GetAll().ProjectTo<HPVendorU01Dto>(_configMapper).ToListAsync();
        }

        public HPVendorU01Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<HPVendorU01Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<HPVendorU01Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }
    }
}