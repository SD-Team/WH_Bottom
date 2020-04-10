using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferLocationController : ControllerBase
    {
        private readonly ITransferLocationService _service;
        public TransferLocationController(ITransferLocationService service)
        {
            _service = service;
        }
        [HttpGet("{qrCodeId}", Name = "GetByQrCode")]
        public async Task<IActionResult> GetByQrCodeId(string qrCodeId)
        {
            var model = await _service.GetByQrCodeId(qrCodeId);
            if (model.QrCodeId != null)
                return Ok(model);
            else return NoContent();
        }

        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok(new {
                a = "aaaaa"
            });
        }
    }
}