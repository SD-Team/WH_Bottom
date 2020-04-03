using AutoMapper;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using System.Threading.Tasks;
using Bottom_API.Helpers;
using Bottom_API._Repositories.Interfaces;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Collections.Generic;

namespace Bottom_API._Services.Services
{
    public class ReceivingService : IReceivingService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IMaterialPurchaseRepository _repoPurchase;
        private readonly IMaterialMissingRepository _repoMissing;

        public ReceivingService(IMaterialPurchaseRepository repoPurchase, IMaterialMissingRepository repoMissing, IMapper mapper, MapperConfiguration configMapper)
        {
            _repoMissing = repoMissing;
            _repoPurchase = repoPurchase;
            _configMapper = configMapper;
            _mapper = mapper;

        }

        public Task<bool> Add(Receiving_Dto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Receiving_Dto>> Filter(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Receiving_Dto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Receiving_Dto GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Receiving_Dto>> GetWithPaginations(PaginationParams param)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedList<Receiving_Dto>> Search(PaginationParams param, object text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Receiving_Dto model)
        {
            throw new System.NotImplementedException();
        }
    }
}