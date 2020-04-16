using System;

namespace Bottom_API.DTO
{
    public class Transaction_Main_Dto
    {
        public int ID { get; set; }
        public string Transac_Type { get; set; }
        public string Transac_No { get; set; }
        public string Transac_Sheet_No { get; set; }
        public string Can_Move { get; set; }
        public DateTime? Transac_Time { get; set; }
        public string QRCode_ID { get; set; }
        public int QRCode_Version { get; set; }
        public string MO_No { get; set; }
        public string Purchase_No { get; set; }
        public string MO_Seq { get; set; }
        public string Material_ID { get; set; }
        public string Material_Name { get; set; }
        public decimal? Purchase_Qty { get; set; }
        public decimal? Transacted_Qty { get; set; }
        public string Rack_Location { get; set; }
        public string Missing_No { get; set; }
        public string Pickup_No { get; set; }
        public DateTime? Updated_Time { get; set; }
        public string Updated_By { get; set; }
    }
}