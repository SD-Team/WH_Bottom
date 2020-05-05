using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMSB_QRCode_Main
    {
        [Key][Column(Order = 0)]
        public string QRCode_ID {get;set;}
        [Key][Column(Order = 1)]
        public int QRCode_Version {get;set;}
        public string QRCode_Type {get;set;}
        public string Receive_No {get;set;}
        public string Valid_Status {get;set;}
        public DateTime? Invalid_Date {get;set;}
        public string Is_Scanned { get; set; }
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
    }
}