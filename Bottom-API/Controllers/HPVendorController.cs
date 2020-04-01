using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HPVendorController : ControllerBase
    {
        private readonly IHPVendorService _server;
        public HPVendorController(IHPVendorService server) {
            _server = server;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAlls() {
            var data = await _server.GetAllAsync();
            return Ok(data);
        }
    }
}