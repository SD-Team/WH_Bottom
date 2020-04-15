namespace Bottom_API.DTO
{
    public class OutputDetail_Dto
    {
        public string QrCodeId { get; set; }
        public string PlanNo { get; set; }
        public string Batch { get; set; }
        public string MatId { get; set; }
        public string MatName { get; set; }
        public string ToolSize { get; set; }
        public decimal? InStockQty { get; set; }
        public decimal? PickupQty { get; set; }
        public decimal? TransOutQty { get; set; }
        public decimal? RemainingQty { get; set; }
    }
}