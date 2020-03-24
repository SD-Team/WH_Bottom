using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Data
{
    public class WMS_DataContext : DbContext
    {
        public WMS_DataContext(DbContextOptions<WMS_DataContext> options) : base(options) { }
        public DbSet<WMSB_CodeID_Detail> WMSB_CodeID_Detail { get; set; }
        public DbSet<WMSB_RackLocation_Main> WMSB_RackLocation_Main { get; set; }
        public DbSet<WMSB_Packing_List> WMSB_Packing_List {get;set;}
        public DbSet<BTW_PackingList_Detail> BTW_PackingList_Detail {get;set;}
        public DbSet<BTW_Packing_Missing> BTW_Packing_Missing {get;set;}
        public DbSet<WMSB_Material_Purchase> WMSB_Material_Purchase {get;set;}
        public DbSet<WMSB_QRCode_Main> WMSB_QRCode_Main {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<BTW_PackingList_Detail>().HasKey(x => new {x.Receive_No, x.PO_Size});
            modelBuilder.Entity<BTW_Packing_Missing>().HasKey(x => new{ x.Missing_No, x.Plan_No });
            modelBuilder.Entity<WMSB_Material_Purchase>().HasKey(x => new {x.Purchase_No, x.MO_No, x.MO_Seq, x.Order_Size, x.Material_ID});
            modelBuilder.Entity<WMSB_QRCode_Main>().HasKey(x => new {x.QRCode_ID, x.QRCode_Version});
        }
    }
}