using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.ViewModel;
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

        [HttpPost("findPrint")]
        public async Task<IActionResult> FindByQRCodeIDList([FromBody]List<QrCodeIDVersion> data) {
            var result = await _service.PrintByQRCodeIDList(data);
            return Ok(result);
        }

        [HttpPost("findPrintQrCode")]
        public async Task<IActionResult> FindByQRCodeID([FromBody]List<string> data) {
            var result = await _service.PrintByQRCodeID(data);
            return Ok(result);
        }

        [HttpPost("findPrintAgain")]
        public async Task<IActionResult> FindByQRCodeIDListAgain([FromBody]List<QrCodeIDVersion> data) {
            var result = await _service.PrintByQRCodeIDListAgain(data);
            return Ok(result);
        }
    }
}