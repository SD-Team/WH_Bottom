namespace Bottom_API.DTO
{
    public class Output_Dto
    {
        public string OutputSheetNo { get; set; }
        public string QrCodeId { get; set; }
        public string PlanNo { get; set; }
        public string SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public string Batch { get; set; }
        public string MatId { get; set; }
        public string MatName { get; set; }
        public string WH { get; set; }
        public string Building { get; set; }
        public string Area { get; set; }
        public string RackLocation { get; set; }
        public decimal? InStockQty { get; set; }
        public decimal? TransOutQty { get; set; }
        public decimal? RemainingQty { get; set; }
    }
}