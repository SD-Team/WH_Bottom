using System;
using System.ComponentModel.DataAnnotations;

namespace Bottom_API.Models
{
    public class WMSB_RackLocation_Main
    {
        [Key]
        public int ID { get; set; }
        public string Rack_Location { get; set; }
        public string Rack_Code { get; set; }
        public string Rack_Level { get; set; }
        public string Rack_Bin { get; set; }
        public string Factory_ID { get; set; }
        public string WH_ID { get; set; }
        public string Build_ID { get; set; }
        public string Floor_ID { get; set; }
        public string Area_ID { get; set; }
        public float? CBM { get; set; }
        public float? Max_per { get; set; }
        public string Memo { get; set; }
        public DateTime? Rack_Invalid_date { get; set; }
        public DateTime? Updated_Time { get; set; }
        public string Updated_By { get; set; }
    }
}