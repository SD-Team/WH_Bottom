using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Data
{
    public class HP_DataContext : DbContext
    {
        public HP_DataContext(DbContextOptions<HP_DataContext> options) : base(options) { }
    }
}