using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackingListDetailController : ControllerBase
    {
        private readonly IPackingListDetailService _service;
        public PackingListDetailController(IPackingListDetailService service) {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAlls() {
            var lists = await _service.GetAllAsync();
            return Ok(lists);
        }

        [HttpGet("findByReceive/{receive_No}")]
        public async Task<IActionResult> FindByReceiveNo(string receive_No) {
            var data = await _service.FindByReceiveNo(receive_No);
            return Ok(data);
        }
    }
}