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
    public class QRCodeDetailService : IQRCodeDetailService
    {
        private readonly IQRCodeDetailRepository _repoQrCodeDetail;
        private readonly IQRCodeMainRepository _repoQrCodeMain;
        private readonly IPackingListDetailRepository _repoPackingDetail;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public QRCodeDetailService( IQRCodeDetailRepository repoQrCodeDetail,
                                    IQRCodeMainRepository repoQrCodeMain,
                                    IPackingListDetailRepository repoPackingDetail,
                                    IMapper mapper,
                                    MapperConfiguration configMapper) {
            _repoQrCodeDetail = repoQrCodeDetail;
            _repoQrCodeMain = repoQrCodeMain;
            _repoPackingDetail = repoPackingDetail;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public Task<bool> Add(QRCode_Detail_Dto model)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<QRCode_Detail_Dto>> GetAllAsync()
        {
            return await _repoQrCodeDetail.GetAll().ProjectTo<QRCode_Detail_Dto>(_configMapper).ToListAsync();
        }

        public QRCode_Detail_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<QRCode_Detail_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<QRCode_Detail_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(QRCode_Detail_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}