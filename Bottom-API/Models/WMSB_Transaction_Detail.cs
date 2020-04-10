using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMSB_Transaction_Detail
    {
        [Key]
        public int ID { get; set; }
        public string Transac_No { get; set; }
        public string Tool_Size { get; set; }
        public string Order_Size { get; set; }
        public string Model_Size { get; set; }
        public string Spec_Size { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Qty { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Trans_Qty { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Instock_Qty { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Untransac_Qty { get; set; }
        public DateTime? Updated_Time { get; set; }
        public string Updated_By { get; set; }


    }
}