using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<WMS_Code> WMS_Code { get; set; }
        public DbSet<WMSB_RackLocation_Main> WMSB_RackLocation_Main { get; set; }
        public DbSet<WMSB_Packing_List> WMSB_Packing_List {get;set;}
        public DbSet<WMSB_PackingList_Detail> WMSB_PackingList_Detail {get;set;}
        public DbSet<BTW_Packing_Missing> BTW_Packing_Missing {get;set;}
        public DbSet<WMSB_Material_Purchase> WMSB_Material_Purchase {get;set;}
        public DbSet<WMSB_QRCode_Main> WMSB_QRCode_Main {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WMS_Code>().HasKey(x => new { x.Code_Type, x.Code_ID });
            modelBuilder.Entity<BTW_Packing_Missing>().HasKey(x => new{ x.Missing_No, x.Plan_No });
            modelBuilder.Entity<WMSB_Material_Purchase>().HasKey(x => new {x.Purchase_No, x.MO_No, x.MO_Seq, x.Order_Size, x.Material_ID});
            modelBuilder.Entity<WMSB_QRCode_Main>().HasKey(x => new {x.QRCode_ID, x.QRCode_Version});
        }
    }
}