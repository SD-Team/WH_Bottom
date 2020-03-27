using System;

namespace Bottom_API.ViewModel
{
    public class QRCodeMainViewModel
    {
        public string QRCode_ID {get;set;}
        public string MO_No {get;set;}
        public string Receive_No {get;set;}
        public DateTime? Receive_Date {get;set;} 
        public string Supplier_ID {get;set;}
        public string Supplier_Name {get;set;}
        public string T3_Supplier {get;set;}
        public string T3_Supplier_Name {get;set;}
        public string Subcon_ID {get;set;}
        public string Subcon_Name {get;set;}
        public string Model_No {get;set;}
        public string Model_Name {get;set;}
        public string Article {get;set;}
        public string MO_Seq {get;set;}
        public string Material_ID {get;set;}
        public string Material_Name {get;set;}
    }
}