using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Data
{
    public class HPDataContext : DbContext
    {
        public HPDataContext(DbContextOptions<HPDataContext> options) : base(options) { }
        public DbSet<HP_Material_j13> HP_Material_j13 {get;set;}
        public DbSet<HP_Style_j08> HP_Style_j08 {get;set;}
        public DbSet<HP_Vendor_u01> HP_Vendor_u01 {get;set;}
    }
}