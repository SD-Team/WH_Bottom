using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMS_Code
    {
        [Key]
        [Column(Order = 0)]
        public int Code_Type { get; set; }
        [Key]
        [Column(Order = 1)]
        public string Code_ID { get; set; }
        public string Code_Cname { get; set; }
        public string Code_Ename { get; set; }
        public string Code_Lname { get; set; }
        public DateTime Updated_Time { get; set; }
        public string Updated_By { get; set; }
    }
}