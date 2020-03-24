using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class BTW_Packing_Missing
    {
        [Key][Column(Order = 0)]
        public string Missing_No {get;set;}
        [Key][Column(Order = 1)]
        public string Plan_No {get;set;}
        public string Pur_No {get;set;}
        public string Batch {get;set;}
        public string PO_Size {get;set;}
        public string Material_No {get;set;}
        public DateTime? Expect_Delivery {get;set;}
        public string Supplier_No {get;set;}
        public string Model_size {get;set;}
        public string Tool_Size {get;set;}
        public string Spec_Size {get;set;}
        public string Tool_Type {get;set;}
        public string Model_No {get;set;}
        public string Model_Name {get;set;}
        public string Article {get;set;}
        public string Subcon_No {get;set;}
        public string Subcon_Name {get;set;}
        public string Vendor_SubNO {get;set;}
        public decimal Pur_Qty {get;set;}
        public decimal Import_Mat {get;set;}
        public string Status {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
    }
}