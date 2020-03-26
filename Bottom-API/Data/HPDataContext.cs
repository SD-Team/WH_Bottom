using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Data
{
    public class HPDataContext : DbContext
    {
        public HPDataContext(DbContextOptions<HPDataContext> options) : base(options) { }
    }
}