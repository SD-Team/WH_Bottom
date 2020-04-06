using System.Collections.Generic;

namespace Bottom_API.ViewModel
{
    public class MaterialMergingViewMode
    {
        public string Order_Size {get;set;}
        public decimal? Purchase_Qty {get;set;}
        public decimal? Accumlated_In_Qty {get;set;}
        public decimal? Delivery_Qty {get;set;}
        public decimal? Delivery_Qty_Batches {get;set;}
        public List<BatchQtyItem> Purchase_Qty_Item {get;set;}
    }
}