using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMSB_QRCode_Detail
    {
        [Key]
        public long QID {get;set;}
        public string QRCode_ID {get;set;}
        public int? QRCode_Version {get;set;}
        public string Tool_Size {get;set;}
        public string Order_Size {get;set;}
        public string Model_Size {get;set;}
        public string Spec_Size {get;set;}
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Qty {get;set;}
        public string Seq {get;set;}
        public string Serial_Num {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
    }
}