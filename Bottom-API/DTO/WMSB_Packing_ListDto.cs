using System;

namespace Bottom_API.DTO
{
    public class WMSB_Packing_ListDto
    {
        public string Sheet_Type {get;set;}
        public string Missing_No {get;set;}
        public string Delivery_No {get;set;}
        public string Supplier_ID {get;set;}
        public string Supplier_Name {get;set;}
        public string Receive_No {get;set;}
        public DateTime? Receive_Date {get;set;}
        public string MO_No {get;set;}
        public string Purchase_No {get;set;}
        public string MO_Seq {get;set;}
        public string Material_ID {get;set;}
        public string Material_Name {get;set;}
        public string Generated_QRCode {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
    }
}