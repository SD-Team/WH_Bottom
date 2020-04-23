using System.Collections.Generic;

namespace Bottom_API.DTO
{
    public class QRCodePrint_Dto
    {
        public List<TransferLocationDetail_Dto> TransactionDetailByQrCodeId { get; set; }
        public Packing_List_Dto PackingListByQrCodeId { get; set; }
        public string RackLocation { get; set; }
    }
}