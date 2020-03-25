using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QRCodeMainController : ControllerBase
    {
        private readonly IQRCodeMainService _service;
        public QRCodeMainController(IQRCodeMainService service) {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAlls() {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddListQRCode([FromBody]List<string> listData) {
            var result = await _service.AddListQRCode(listData);
            return Ok(result);
        }

    }
}