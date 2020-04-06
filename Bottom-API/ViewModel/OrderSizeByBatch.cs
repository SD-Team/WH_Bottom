using System.Collections.Generic;

namespace Bottom_API.ViewModel
{
    public class OrderSizeByBatch
    {
        public string MO_Seq {get;set;}
        public string Purchase_No {get;set;}
        public string Missing_No {get;set;}
        // public List<decimal?> Purchase_Qty {get;set;}
        public List<OrderSizeAccumlate> Purchase_Qty {get;set;}
    }
}