using System.Threading.Tasks;
using Bottom_API._Repositories.Interface;
using Bottom_API.Data;
using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        // public async Task<MES_User> Login(string username, string password)
        // {
        //     var user = await _context.MES_User.FirstOrDefaultAsync(x => x.User_ID == username);
        //     if (user == null)
        //         return null;

        //     if (user.Password != password)
        //         return null;
        //     else return user;
        // }
    }
}