using System.Threading.Tasks;
using Bottom_API.DTO;

namespace Bottom_API._Services.Interfaces
{
    public interface IAuthService
    {
         Task<UserForLogged_Dto> GetUser(string username);
    }
}