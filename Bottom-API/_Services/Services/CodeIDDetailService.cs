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
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class CodeIDDetailService : ICodeIDDetailService
    {
        private readonly ICodeIDDetailRepo _repoCodeIDDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public CodeIDDetailService(ICodeIDDetailRepo repoCodeIDDetail, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoCodeIDDetail = repoCodeIDDetail;

        }
        public async Task<bool> Add(CodeID_Detail_Dto model)
        {
            var item = _mapper.Map<WMS_Code>(model);
            _repoCodeIDDetail.Add(item);
            return await _repoCodeIDDetail.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var item = _repoCodeIDDetail.FindById(id);
            _repoCodeIDDetail.Remove(item);
            return await _repoCodeIDDetail.SaveAll();
        }

        public async Task<List<CodeID_Detail_Dto>> GetAllAsync()
        {
            return await _repoCodeIDDetail.FindAll().ProjectTo<CodeID_Detail_Dto>(_configMapper).OrderByDescending(x => x.Updated_Time).ToListAsync();
        }

        public async Task<List<CodeID_Detail_Dto>> GetArea()
        {
            return await _repoCodeIDDetail.FindAll().ProjectTo<CodeID_Detail_Dto>(_configMapper).Where(x => x.Code_Type == 5).ToListAsync();
        }

        public async Task<List<CodeID_Detail_Dto>> GetBuilding()
        {
            return await _repoCodeIDDetail.FindAll().ProjectTo<CodeID_Detail_Dto>(_configMapper).Where(x => x.Code_Type == 3).ToListAsync();
        }

        public CodeID_Detail_Dto GetById(object id)
        {
            return _mapper.Map<WMS_Code, CodeID_Detail_Dto>(_repoCodeIDDetail.FindById(id));
        }

        public async Task<List<CodeID_Detail_Dto>> GetFactory()
        {
            return await _repoCodeIDDetail.FindAll().ProjectTo<CodeID_Detail_Dto>(_configMapper).Where(x => x.Code_Type == 1).ToListAsync();
        }

        public async Task<List<CodeID_Detail_Dto>> GetFloor()
        {
            return await _repoCodeIDDetail.FindAll().ProjectTo<CodeID_Detail_Dto>(_configMapper).Where(x => x.Code_Type == 4).ToListAsync();
        }

        public async Task<List<CodeID_Detail_Dto>> GetWH()
        {
            return await _repoCodeIDDetail.FindAll().ProjectTo<CodeID_Detail_Dto>(_configMapper).Where(x => x.Code_Type == 2).ToListAsync();
        }

        public Task<PagedList<CodeID_Detail_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<CodeID_Detail_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(CodeID_Detail_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}