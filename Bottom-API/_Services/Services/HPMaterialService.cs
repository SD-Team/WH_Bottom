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
    public class HPMaterialService : IHPMaterialService
    {
        private readonly IHPMaterialRepository _repo;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public HPMaterialService(
                                    IHPMaterialRepository repo,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repo = repo;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public Task<bool> Add(HP_Material_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HP_Material_Dto>> GetAllAsync()
        {
            var lists = await _repo.GetAll().ProjectTo<HP_Material_Dto>(_configMapper).ToListAsync();
            return lists;
        }

        public HP_Material_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<HP_Material_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<HP_Material_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(HP_Material_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}