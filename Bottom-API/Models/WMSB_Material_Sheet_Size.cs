using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bottom_API.Models
{
    public class WMSB_Material_Sheet_Size
    {
        public string Factory_ID { get; set; }
        [Key][Column(Order = 0)]
        public string Manno { get; set; }
        [Key][Column(Order = 1)]
        public Int16 Cur_Ent { get; set; }
        public string Batch { get; set; }
        [Key][Column(Order = 2)]
        public string Material_ID { get; set; }
        public string Sheet_No { get; set; }
        [Key][Column(Order = 3)]
        public string Order_Size { get; set; }
        public string Model_Size { get; set; }
        public string Tool_Size { get; set; }
        public decimal Qty { get; set; }
        public string Print_Size { get; set; }
        public string Subcont_Material { get; set; }
        public string HP_User { get; set; }
        public DateTime? HP_Update_Time { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Time { get; set; }
    }
}