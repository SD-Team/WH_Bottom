using System;

namespace Bottom_API.DTO
{
    public class Material_Sheet_Size_Dto
    {
        public string Factory_ID { get; set; }
        public string Manno { get; set; }
        public Int16 Cur_Ent { get; set; }
        public string Batch { get; set; }
        public string Material_ID { get; set; }
        public string Sheet_No { get; set; }
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
        public string Prod_Delivery_Way { get; set; }
    }
}