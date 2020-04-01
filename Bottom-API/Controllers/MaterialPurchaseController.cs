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
        // public async Task<IActionResult> SearchByModel([FromQuery]PaginationParams param ,MaterialSearchViewModel model) {
        //     var lists = await _server.SearchByModel(param, model);
        //     Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
        //     return Ok(lists);
        // }
        public async Task<IActionResult> SearchByModel([FromBody]MaterialSearchViewModel model) {
            var lists = await _server.SearchByModel(model);
            return Ok(lists);
        }

        [HttpGet("search/{Purchase_No}")]
        public async Task<IActionResult> FunctionTest(string Purchase_No) {
            var data = await _server.MaterialMerging(Purchase_No);
            return Ok(data);   
        }
    }
}