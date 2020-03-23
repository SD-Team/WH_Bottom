using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface ICodeIDDetailService : IBottomService<CodeID_Detail_Dto>
    {
        Task<List<CodeID_Detail_Dto>> GetFactory();
        Task<List<CodeID_Detail_Dto>> GetWH();
        Task<List<CodeID_Detail_Dto>> GetBuilding();
        Task<List<CodeID_Detail_Dto>> GetFloor();
        Task<List<CodeID_Detail_Dto>> GetArea();
    }
}