using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialPurchaseController : ControllerBase
    {
        private readonly IMaterialPurchaseService _server;
        public MaterialPurchaseController(IMaterialPurchaseService server) {
            _server = server;
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAlls() {
            var data = await _server.GetAllAsync();
            return Ok(data);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchByModel([FromQuery]PaginationParams param ,MaterialSearchViewModel model) {
            var lists = await _server.SearchByModel(param, model);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }
    }
}