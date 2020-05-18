using System;

namespace Bottom_API.DTO
{
    public class TransferLocation_Dto
    {
        public long Id { get; set; }
        public string TransferNo { get; set; }
        public string QrCodeId { get; set; }
        public string PlanNo { get; set; }
        public string ReceiveNo { get; set; }
        public string Batch { get; set; }
        public string MatId { get; set; }
        public string MatName { get; set; }
        public decimal? Qty { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? TransacTime { get; set; }
        public string TransacType { get; set; }
        public string ModelName { get; set; }
        public string ModelNo { get; set; }
        public string Article { get; set; }
        public string CustmoerPart { get; set; }
        public string CustmoerName { get; set; }
    }
}