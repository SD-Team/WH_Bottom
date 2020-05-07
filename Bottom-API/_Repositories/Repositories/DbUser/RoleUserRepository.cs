using Bottom_API._Repositories.Interfaces.DbUser;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories.DbUser
{
    public class RoleUserRepository : DbUserRepository<RoleUser>, IRoleUserRepository
    {
        public RoleUserRepository(UserContext context) : base(context)
        {
        }
    }
}