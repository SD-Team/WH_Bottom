using AutoMapper;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Services.Interfaces;
using System.Threading.Tasks;
using Bottom_API.DTO;
using System.Collections.Generic;
using Bottom_API.Helpers;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class RackLocationService : IRackLocationService
    {
        private readonly IRackLocationRepo _repoRackLocation;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public RackLocationService(IRackLocationRepo repoRackLocation, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoRackLocation = repoRackLocation;

        }

        public Task<bool> Add(RackLocation_Main_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public  Task<List<RackLocation_Main_Dto>> Filter(FilterRackLocationParam filterParam)
        {
            // var result =  await _repoRackLocation.FindAll().ProjectTo<RackLocation_Main_Dto>(_configMapper).OrderByDescending(x => x.Updated_Time).ToListAsync();
            // if(filterParam.factory)
            throw new System.NotImplementedException();
        }

        public Task<List<RackLocation_Main_Dto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public RackLocation_Main_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<RackLocation_Main_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<RackLocation_Main_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(RackLocation_Main_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}