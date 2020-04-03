using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceivingController : ControllerBase
    {
        private readonly IReceivingService _service;
        public ReceivingController(IReceivingService service)
        {
            _service = service;
        }
        
        [HttpPost("search")]
        public async Task<IActionResult> SearchByModel([FromBody]MaterialSearchViewModel model) {
            var lists = await _service.SearchByModel(model);
            return Ok(lists);
        }

        [HttpPost("searchTable")]
        public async Task<IActionResult> SearchByPurchase([FromBody]MaterialMainViewModel model) {
            var data = await _service.MaterialMerging(model);
            return Ok(data);   
        }
    }
}