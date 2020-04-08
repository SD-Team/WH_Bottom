namespace Bottom_API.ViewModel
{
    public class OrderSizeAccumlate
    {
        public string Order_Size {get;set;}
        public string Model_Size {get;set;}
        public string Tool_Size {get;set;}
        public string Spec_Size {get;set;}
        public decimal? Purchase_Qty {get;set;}
        public decimal? Purchase_Qty_Const {get;set;}
        public decimal? Accumlated_In_Qty {get;set;}
        public decimal? Received_Qty {get;set;}
        public decimal? MO_Qty {get;set;}
    }
}