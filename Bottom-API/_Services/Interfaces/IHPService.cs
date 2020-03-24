using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.Helpers;

namespace Bottom_API._Services.Interfaces
{
    public interface IHPService<T> where T: class
    {
        Task<List<T>> GetAllAsync();

        Task<PagedList<T>> GetWithPaginations(PaginationParams param);

        Task<PagedList<T>> Search(PaginationParams param, object text);
        T GetById(object id);
    }
}