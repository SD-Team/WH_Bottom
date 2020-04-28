using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackingListController : ControllerBase
    {
        private readonly IPackingListService _service;
        public PackingListController(IPackingListService service) {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAlls() {
            var lists = await _service.GetAllAsync();
            return Ok(lists);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromQuery]PaginationParams param, FilterPackingListParam filterParam) {
            var lists = await _service.SearchViewModel(param, filterParam);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }

        [HttpGet("findBySupplier/{supplier_ID}")]
        public async Task<IActionResult> FindBySupplier(string supplier_ID) {
            var data = await _service.FindBySupplier(supplier_ID);
            return Ok(new {supplierName = data});
        }
    }
}