using Bottom_API._Repositories.Interfaces.DbUser;
using Bottom_API.Data;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Repositories.DbUser
{
    public class RolesRepository : DbUserRepository<Roles>, IRolesRepository
    {
        public RolesRepository(UserContext context) : base(context)
        {
        }
    }
}