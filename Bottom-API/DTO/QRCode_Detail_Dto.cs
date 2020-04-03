using System;

namespace Bottom_API.DTO
{
    public class QRCode_Detail_Dto
    {
        public long QID {get;set;}
        public string QRCode_ID {get;set;}
        public int QRCode_Version {get;set;}
        public string Tool_Size {get;set;}
        public string Order_Size {get;set;}
        public string Model_Size {get;set;}
        public string Spec_Size {get;set;}
        public decimal? Qty {get;set;}
        public string Seq {get;set;}
        public string Serial_Num {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
        public QRCode_Detail_Dto() 
        {
            this.Updated_Time = DateTime.Now;
        }
    }
}