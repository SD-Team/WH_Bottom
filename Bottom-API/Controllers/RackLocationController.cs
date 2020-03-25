using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [Route("api/[controller]")]
    public class RackLocationController : ControllerBase
    {
        private readonly IRackLocationService _service;
        public RackLocationController(IRackLocationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Filter([FromQuery]PaginationParams param, FilterRackLocationParam filterParam) 
        {
            var result = await _service.Filter(filterParam);
            return Ok(result);
        }
    }
}