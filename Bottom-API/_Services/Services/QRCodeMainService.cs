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
    public class QRCodeMainService : IQRCodeMainService
    {
        private readonly IQRCodeMainRepository _repo;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public QRCodeMainService(   IQRCodeMainRepository repo,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repo = repo;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<List<WMSB_QRCode_MainDto>> GetAllAsync()
        {
            return await _repo.GetAll().ProjectTo<WMSB_QRCode_MainDto>(_configMapper).ToListAsync();
        }

        public WMSB_QRCode_MainDto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<WMSB_QRCode_MainDto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<WMSB_QRCode_MainDto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }
    }
}