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
using Bottom_API.Models;
using System;

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

        public async Task<bool> Add(RackLocation_Main_Dto model)
        {
            var item = _mapper.Map<WMSB_RackLocation_Main>(model);
            _repoRackLocation.Add(item);
            return await _repoRackLocation.SaveAll();
        }

        public async Task<bool> Update(RackLocation_Main_Dto model)
        {
            var item = _mapper.Map<WMSB_RackLocation_Main>(model);
            item.Updated_Time = DateTime.Now;
            _repoRackLocation.Update(item);
            return await _repoRackLocation.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var item = _repoRackLocation.FindById(id);
            _repoRackLocation.Remove(item);
            return await _repoRackLocation.SaveAll();
        }

        public async  Task<PagedList<RackLocation_Main_Dto>> Filter(PaginationParams param, FilterRackLocationParam filterParam)
        {
            var resultAll =  _repoRackLocation.FindAll().ProjectTo<RackLocation_Main_Dto>(_configMapper);
            if(filterParam.Factory != "" && filterParam.Factory != null)
            {
                resultAll = resultAll.Where(x => x.Factory_ID == filterParam.Factory);
            }

            if (filterParam.Wh != "" && filterParam.Wh != null)
            {
                resultAll = resultAll.Where(x => x.WH_ID == filterParam.Wh);
            }

            if (filterParam.Building != "" && filterParam.Building != null)
            {
                resultAll = resultAll.Where(x => x.Build_ID == filterParam.Building);
            }

            if (filterParam.Floor != "" && filterParam.Floor != null)
            {
                resultAll = resultAll.Where(x => x.Floor_ID == filterParam.Floor);
            }

            if (filterParam.Area != "" && filterParam.Area != null)
            {
                resultAll = resultAll.Where(x => x.Area_ID == filterParam.Area);
            }
            resultAll = resultAll.OrderByDescending(x => x.Updated_Time);
            return await PagedList<RackLocation_Main_Dto>.CreateAsync(resultAll, param.PageNumber, param.PageSize);
           // return await resultAll.OrderByDescending(x => x.Updated_Time).ToListAsync();
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


    }
}