using System.Collections.Generic;

namespace Bottom_API.ViewModel
{
    public class OrderSizeByBatch
    {
        public string MO_Seq {get;set;}
        public string Purchase_No {get;set;}
        public string Missing_No {get;set;}
        public string CheckInsert {get;set;}
        public string MO_No {get;set;}
        public string Delivery_No {get;set;}
        public string Material_ID {get;set;}
        public string Material_Name {get;set;}
        public string Model_No {get;set;}
        public string Model_Name {get;set;}
        public string Article {get;set;}
        public string Supplier_ID {get;set;}
        public string Supplier_Name {get;set;}
        public string Subcon_No {get;set;}
        public string Subcon_Name {get;set;}
        public string T3_Supplier {get;set;}
        public string T3_Supplier_Name {get;set;}
        public List<OrderSizeAccumlate> Purchase_Qty {get;set;}
    }
}