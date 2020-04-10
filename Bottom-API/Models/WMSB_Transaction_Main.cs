using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMSB_Transaction_Main
    {
        [Key]
        public int ID { get; set; }
        public string Transac_Type { get; set; }
        public string Transac_No { get; set; }
        public string Transac_Sheet_No { get; set; }
        public string Can_Move { get; set; }
        public DateTime? Transac_Time { get; set; }
        public string QRCode_ID { get; set; }
        public int QRCode_Version { get; set; }
        public string MO_No { get; set; }
        public string Purchase_No { get; set; }
        public string MO_Seq { get; set; }
        public string Material_ID { get; set; }
        public string Material_Name { get; set; }
        
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Transacted_Qty { get; set; }
        public string Rack_Location { get; set; }
        public string Missing_No { get; set; }
        public string Pickup_No { get; set; }
        public DateTime? Updated_Time { get; set; }
        public string Updated_By { get; set; }
        public string Can_Move { get; set; }
    }
}