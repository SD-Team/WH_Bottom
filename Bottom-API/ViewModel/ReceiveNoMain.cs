using System;

namespace Bottom_API.ViewModel
{
    public class ReceiveNoMain
    {
        public string MO_No {get;set;}
        public string Purchase_No {get;set;}
        public string Delivery_No {get;set;}
        public string Missing_No {get;set;}
        public string Receive_No {get;set;}
        public string MO_Seq {get;set;}
        public string Material_ID {get;set;}
        public string Material_Name {get;set;}
        public DateTime? Receive_Date {get;set;}
        public decimal? Purchase_Qty {get;set;}
        public decimal? Accumated_Qty {get;set;}
        public decimal? Accumated_Qty_All {get;set;}
        public string Generated_QRCode {get;set;}
        public string Sheet_Type {get;set;}
        public string Updated_By {get;set;}
    }
}