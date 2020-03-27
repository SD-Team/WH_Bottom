using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QRCodeDetailController : ControllerBase
    {
        private readonly IQRCodeDetailService _service;
        public QRCodeDetailController(IQRCodeDetailService service) {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAlls() {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

    }
}