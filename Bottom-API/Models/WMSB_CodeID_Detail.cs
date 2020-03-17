using System;
using System.ComponentModel.DataAnnotations;

namespace Bottom_API.Models
{
    public class WMSB_CodeID_Detail
    {
        [Key]
        public int Code_ID { get; set; }
        public string Factory_ID { get; set; }
        public string Factory_Name { get; set; }
        public string WH_ID { get; set; }
        public string WH_Name { get; set; }
        public string Build_ID { get; set; }
        public string Build_Name { get; set; }
        public string Floor_ID { get; set; }
        public string Floor_Name { get; set; }
        public string Area_ID { get; set; }
        public string Area_Name { get; set; }
        public DateTime Updated_Time { get; set; }
        public string Updated_By { get; set; }
    }
}