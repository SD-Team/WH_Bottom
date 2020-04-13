namespace Bottom_API.ViewModel
{
    public class MaterialEditModel
    {
        public string Purchase_No {get;set;}
        public string Missing_No {get;set;}
        public string Receive_No {get;set;}
        public string Order_Size {get;set;}
        public string MO_Seq_Edit {get;set;}
        public decimal? Purchase_Qty {get;set;}
        public decimal? Accumated_Qty {get;set;}
        public decimal? Delivery_Qty {get;set;}
        public decimal? Received_Qty {get;set;}
        public decimal Received_Qty_Edit {get;set;}
    }
}