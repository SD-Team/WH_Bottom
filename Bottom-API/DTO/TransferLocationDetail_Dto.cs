using System;

namespace Bottom_API.DTO
{
    public class TransferLocationDetail_Dto
    {
        public int ID { get; set; }
        public string Transac_No { get; set; }
        public string Tool_Size { get; set; }
        public string Order_Size { get; set; }
        public string Model_Size { get; set; }
        public string Spec_Size { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Trans_Qty { get; set; }
        public decimal? Instock_Qty { get; set; }
        public decimal? Untransac_Qty { get; set; }
        public DateTime? Updated_Time { get; set; }
        public string Updated_By { get; set; }
    }
}