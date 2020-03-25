using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMSB_Material_Purchase
    {
        public string Factory_ID {get;set;}
        [Key][Column(Order = 0)]
        public string Purchase_No {get;set;}
        public string Collect_No {get;set;}
        [Key][Column(Order = 1)]
        public string MO_No {get;set;}
        [Key][Column(Order = 2)]
        public string MO_Seq {get;set;}
        [Key][Column(Order = 3)]
        public string Order_Size {get;set;}
        public string Material_ID {get;set;}
        public string Model_Size {get;set;}
        public string Tool_Size {get;set;}
        public string Spec_Size {get;set;}
        public string Purchase_Kind {get;set;}
        public string Purchase_Size {get;set;}

        [Column(TypeName = "decimal(9,2)")]
        public decimal MO_Qty {get;set;}

        [Column(TypeName = "decimal(9,2)")]
        public decimal PreBook_Qty {get;set;}

        [Column(TypeName = "decimal(9,2)")]
        public decimal Stock_Qty {get;set;}

        [Column(TypeName = "decimal(9,2)")]
        public decimal Purchase_Qty {get;set;}
        
        [Column(TypeName = "decimal(9,2)")]
        public decimal Accumlated_In_Qty { get; set; }
        public string Supplier_ID {get;set;}
        public DateTime? Require_Delivery {get;set;}
        public DateTime? Confirm_Delivery {get;set;}
        public string Process_Code {get;set;}
        public string Custmoer_Part {get;set;}
        public string T3_Supplier {get;set;}
        public string Stage {get;set;}
        public string T3_Purchase_No {get;set;}
        public string HP_User {get;set;}
        public DateTime? HP_Time {get;set;}
        public string Update_By {get;set;}
        public DateTime? Update_Time {get;set;}
    }
}