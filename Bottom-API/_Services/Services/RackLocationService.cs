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

        public async  Task<List<RackLocation_Main_Dto>> Filter(FilterRackLocationParam filterParam)
        {
            var resultAll =  _repoRackLocation.FindAll().ProjectTo<RackLocation_Main_Dto>(_configMapper);
            if(filterParam.factory != "")
            {
                resultAll = resultAll.Where(x => x.Factory_ID == filterParam.factory);
            }

            if (filterParam.wh != "")
            {
                resultAll = resultAll.Where(x => x.WH_ID == filterParam.wh);
            }

            if (filterParam.building != "")
            {
                resultAll = resultAll.Where(x => x.Build_ID == filterParam.building);
            }

            if (filterParam.floor != "")
            {
                resultAll = resultAll.Where(x => x.Floor_ID == filterParam.floor);
            }

            if (filterParam.area != "")
            {
                resultAll = resultAll.Where(x => x.Area_ID == filterParam.area);
            }

            return await resultAll.OrderByDescending(x => x.Updated_Time).ToListAsync();
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