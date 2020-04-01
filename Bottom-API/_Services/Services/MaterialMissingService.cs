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
    public class MaterialMissingService : IMaterialMissingService
    {
        private readonly IMaterialMissingRepository _repo;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public MaterialMissingService(  IMaterialMissingRepository repo,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repo = repo;
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

        public async Task<List<Material_Dto>> GetAllAsync()
        {
            var lists = await _repo.GetAll().ProjectTo<Material_Dto>(_configMapper)
                    .ToListAsync();
            return lists;
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

        public Task<bool> Update(Material_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}