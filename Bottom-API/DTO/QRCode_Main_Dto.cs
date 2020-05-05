using System;

namespace Bottom_API.DTO
{
    public class QRCode_Main_Dto
    {
        public string QRCode_ID {get;set;}
        public int QRCode_Version {get;set;}
        public string QRCode_Type {get;set;}
        public string Receive_No {get;set;}
        public string Valid_Status {get;set;}
        public DateTime? Invalid_Date {get;set;}
        public string Is_Scanned {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
        public QRCode_Main_Dto() 
        {
            this.Updated_Time = DateTime.Now;
        }
    }
}