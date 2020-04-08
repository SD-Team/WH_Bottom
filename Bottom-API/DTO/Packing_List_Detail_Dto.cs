using System;

namespace Bottom_API.DTO
{
    public class Packing_List_Detail_Dto
    {
        public long SID { get; set; }
        public string Receive_No {get;set;}
        public string Order_Size { get; set; }
        public string Model_Size { get; set; }
        public string Tool_Size {get;set;}
        public string Spec_Size { get; set; }
        public decimal? MO_Qty {get;set;}
        public decimal? Purchase_Qty { get; set; }
        public decimal? Received_Qty {get;set;}
        public DateTime? Updated_Time {get;set;}
        public string Updated_By {get;set;}
        public Packing_List_Detail_Dto() {
            this.Updated_Time = DateTime.Now;
        }
    }
}