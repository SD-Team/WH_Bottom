using System.Threading.Tasks;
using Bottom_API._Repositories.Interfaces.DbUser;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;

namespace Bottom_API._Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _repoUsers;
        public AuthService(IUsersRepository repoUsers)
        {
            _repoUsers = repoUsers;
        }
        public async Task<UserForLogged_Dto> GetUser(string username)
        {
            var user = _repoUsers.FindSingle(x => x.Username.Trim() == username.Trim());
            // kiểm tra xem username đó có ko
            if (user == null)
            {
                return null;
            }
            var result = new UserForLogged_Dto {
                Id  = user.Id,
                Email = user.Email,
                Username = user.Username,
                Name = user.Name,
                Nik = user.Nik
            };
            return result;
        }
    }
}