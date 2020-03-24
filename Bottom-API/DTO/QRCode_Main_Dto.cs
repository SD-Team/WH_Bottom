using System;

namespace Bottom_API.DTO
{
    public class QRCode_Main_Dto
    {
        public string QRCode_ID {get;set;}
        public int QRCode_Version {get;set;}
        public string QRCode_Type {get;set;}
        public string Receive_No {get;set;}
        public string Refer_Type {get;set;}
        public string Valid_Status {get;set;}
        public DateTime? Invalid_Date {get;set;}
        public DateTime? Create_Time {get;set;}
        public string Create_By {get;set;}
        public DateTime? Update_Time {get;set;}
        public string Update_By {get;set;}
    }
}