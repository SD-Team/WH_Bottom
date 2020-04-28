using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API.Models;

namespace Bottom_API._Repositories.Interfaces
{
    public interface ICodeIDDetailRepo : IBottomRepository<WMS_Code>
    {
        string GetBuildingName(string buildingId);
        string GetAreaName(string areaId);
        string GetFloorName(string floorId);
    }
}