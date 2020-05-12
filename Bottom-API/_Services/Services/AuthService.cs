using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bottom_API._Repositories.Interfaces.DbUser;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _repoUsers;
        private readonly IRolesRepository _repoRoles;
        private readonly IRoleUserRepository _repoRoleUser;
        public AuthService(IUsersRepository repoUsers, IRolesRepository repoRoles, IRoleUserRepository repoRoleUser)
        {
            _repoRoleUser = repoRoleUser;
            _repoRoles = repoRoles;
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

            var roleUser = _repoRoleUser.FindAll(x => x.UserId == user.Id); 
            var role = _repoRoles.FindAll(x => x.WH_Type == "B");
            var roleName = await roleUser.Join(role, x => x.RoleId, y => y.Id, (x, y) => new Role_Dto {Name = y.RoleUnique, Position = y.RoleSeq}).ToListAsync();

            var result = new UserForLogged_Dto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Name = user.Name,
                Nik = user.Nik,
                Role = roleName.OrderBy(x => x.Position).Select(x => x.Name).ToList()
            };

            return result;
        }
    }
}