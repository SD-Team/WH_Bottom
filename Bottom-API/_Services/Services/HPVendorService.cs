using System.Collections.Generic;
using System.Linq;
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
    public class HPVendorService : IHPVendorService
    {
        private readonly IHPVendorRepository _repo;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public HPVendorService(  IHPVendorRepository repo,
                                IMapper mapper,
                                MapperConfiguration configMapper) {
            _repo = repo;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public Task<bool> Add(HP_Vendor_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HP_Vendor_Dto>> GetAllAsync()
        {
            var lists =  await _repo.GetAll().ProjectTo<HP_Vendor_Dto>(_configMapper).Take(100)
                .ToListAsync();
            return lists;
        }

        public HP_Vendor_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<HP_Vendor_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<HP_Vendor_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(HP_Vendor_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}