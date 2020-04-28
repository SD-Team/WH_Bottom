using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bottom_API._Repositories.Interfaces;
using Bottom_API.Data;
using Bottom_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API._Repositories.Repositories
{
    public class CodeIDDetailRepo : BottomRepository<WMS_Code>, ICodeIDDetailRepo
    {
        private readonly DataContext _context;
        public CodeIDDetailRepo(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<WMS_Code>> GetArea()
        {
            var list = await _context.WMS_Code.Where(x => x.Code_Type == 5).ToListAsync();
            return list;
        }

        public async Task<List<WMS_Code>> GetBuilding()
        {
            var list = await _context.WMS_Code.Where(x => x.Code_Type == 3).ToListAsync();
            return list;
        }

        public async Task<List<WMS_Code>> GetFactory()
        {
            var list = await _context.WMS_Code.Where(x => x.Code_Type == 1).ToListAsync();
            return list;
        }

        public async Task<List<WMS_Code>> GetFloor()
        {
            var list = await _context.WMS_Code.Where(x => x.Code_Type == 4).ToListAsync();
            return list;
        }

        public async Task<List<WMS_Code>> GetWH()
        {
            var list = await _context.WMS_Code.Where(x => x.Code_Type == 2).ToListAsync();
            return list;
        }

        public string GetBuildingName(string buildingId)
        {
            var data = _context.WMS_Code.Where(x => x.Code_Type == 3 && x.Code_ID == buildingId).FirstOrDefault();
            return data.Code_Ename;
        }
        public string GetAreaName(string areaId)
        {
            var data = _context.WMS_Code.Where(x => x.Code_Type == 5 && x.Code_ID == areaId).FirstOrDefault();
            return data.Code_Ename;
        }
        public string GetFloorName(string floorId)
        {
            var data = _context.WMS_Code.Where(x => x.Code_Type == 4 && x.Code_ID == floorId).FirstOrDefault();
            return data.Code_Ename;
        }
    }
}