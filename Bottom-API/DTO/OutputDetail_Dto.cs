using System.Collections.Generic;

namespace Bottom_API.DTO
{
    public class OutputDetail_Dto
    {
        public long Id { get; set; }
        public string QrCodeId { get; set; }
        public string PlanNo { get; set; }
        public string Batch { get; set; }
        public string MatId { get; set; }
        public string MatName { get; set; }
        public List<TransferLocationDetail_Dto> TransactionDetail { get; set; }
    }
}