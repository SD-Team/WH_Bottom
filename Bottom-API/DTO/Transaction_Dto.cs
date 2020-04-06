namespace Bottom_API.DTO
{
    public class Transaction_Dto
    {
        public string Input_No { get; set; }
        public string QrCode_Id { get; set; }
        public string Plan_No { get; set; }
        public string Suplier_No { get; set; }
        public string Suplier_Name { get; set; }
        public string Batch { get; set; }
        public decimal? Accumated_Qty { get; set; }
        public decimal? Trans_In_Qty { get; set; }
        public decimal? InStock_Qty { get; set; }
        public string Mat_Id { get; set; }
        public string Mat_Name { get; set; }

    }
}