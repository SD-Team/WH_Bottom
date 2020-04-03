using System;
using System.ComponentModel.DataAnnotations;

namespace Bottom_API.Models
{
    public class WMSB_Transaction_Main
    {
        [Key]
        public int ID { get; set; }
        public string Transac_Type { get; set; }
        public string Transac_No { get; set; }
        public string Transac_Sheet_No { get; set; }
        public DateTime Transac_Time { get; set; }
        public string QRCode_ID { get; set; }
        public int QRCode_Version { get; set; }
        public decimal? Transacted_Qty { get; set; }
        public string Source_Location { get; set; }
        public string Dest_Location { get; set; }
        public string Missing_No { get; set; }
        public string Pickup_No { get; set; }
        public DateTime Updated_Time { get; set; }
        public string Updated_By { get; set; }
    }
}