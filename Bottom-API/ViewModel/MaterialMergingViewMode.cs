using System.Collections.Generic;

namespace Bottom_API.ViewModel
{
    public class MaterialMergingViewMode
    {
        public string Order_Size {get;set;}
        public decimal? Purchase_Qty {get;set;}
        public List<BatchQtyItem> Purchase_Qty_Item {get;set;}
    }
}