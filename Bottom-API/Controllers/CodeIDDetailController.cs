using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [Route("api/[controller]")]
    public class CodeIDDetailController : ControllerBase
    {
        private readonly ICodeIDDetailService _service;
        public CodeIDDetailController(ICodeIDDetailService service)
        {
            _service = service;
        }

        [HttpGet("factory", Name = "GetFactory")]
        public async Task<IActionResult> GetFactory()
        {
            var model = await _service.GetFactory();
            return Ok(model);
        }

        [HttpGet("wh", Name = "GetWH")]
        public async Task<IActionResult> GetAll()
        {
            var wh = await _service.GetWH();
            return Ok(wh);
        }

        [HttpGet("building", Name = "GetBuilding")]
        public async Task<IActionResult> GetBuilding()
        {
            var wh = await _service.GetBuilding();
            return Ok(wh);
        }

        [HttpGet("floor", Name = "GetFloor")]
        public async Task<IActionResult> GetFloor()
        {
            var wh = await _service.GetFloor();
            return Ok(wh);
        }

        [HttpGet("area", Name = "GetArea")]
        public async Task<IActionResult> GetArea()
        {
            var wh = await _service.GetArea();
            return Ok(wh);
        }
    }
}