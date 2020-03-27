using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMSB_Packing_List
    {
        public string Sheet_Type {get;set;}
        public string Missing_No {get;set;}
        public string Delivery_No {get;set;}
        public string Supplier_ID {get;set;}
        public string Supplier_Name {get;set;}

        [Key]
        public string Receive_No {get;set;}
        public DateTime? Receive_Date {get;set;}
        public string MO_No {get;set;}
        public string Purchase_No {get;set;}
        public string MO_Seq {get;set;}
        public string Material_ID {get;set;}
        public string Material_Name {get;set;}
        public string Model_No {get;set;}
        public string Model_Name {get;set;}
        public string Article {get;set;}
        public string Subcon_ID {get;set;}
        public string Subcon_Name {get;set;}
        public string T3_Supplier {get;set;}
        public string T3_Supplier_Name {get;set;}
        public string Generated_QRCode {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
    }
}