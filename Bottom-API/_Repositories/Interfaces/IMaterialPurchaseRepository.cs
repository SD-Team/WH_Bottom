using System.Collections.Generic;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface IMaterialPurchaseRepository : IBottomRepository<WMSB_Material_Purchase>
    {
        List<string> GetPurchase();

        List<WMSB_Material_Purchase> GetFactory(string Purchase_No, string MO_No, string MO_Seq, string Material_ID);
    }
}