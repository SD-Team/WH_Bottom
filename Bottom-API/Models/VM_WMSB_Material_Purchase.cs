using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class VM_WMSB_Material_Purchase
    {
        public string Factory_ID {get;set;}

        [Key]
        [Column(Order = 0)]
        public string Plan_No {get;set;}
        [Key]
        [Column(Order = 1)]
        public string Purchase_No {get;set;}

        [Key]
        [Column("Mat#", Order = 2)]
        public string Mat_ {get;set;}

        [Column("Mat#_Name")]
        public string Mat__Name {get;set;}
        public string Model_No {get;set;}
        public string Model_Name {get;set;}
        public string Article {get;set;}
        public string Supplier_No {get;set;}
        public string Supplier_Name {get;set;}
        public string Subcon_No {get;set;}
        public string Subcon_Name {get;set;}
        public string T3_Supplier {get;set;}
        public string T3_Supplier_Name {get;set;}
        public string Custmoer_Part {get;set;}
        public string Custmoer_Name {get;set;}
    }
}