using Bottom_API._Repositories.Interfaces.DbUser;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories.DbUser
{
    public class UsersRepository : DbUserRepository<Users>, IUsersRepository
    {
        public UsersRepository(UserContext context) : base(context)
        {
        }
    }
}