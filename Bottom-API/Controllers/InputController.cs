using System;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InputController : ControllerBase
    {
        
        private readonly IInputService _service;
        public InputController(IInputService service)
        {
            _service = service;
        }

        [HttpGet("{qrCodeID}", Name="GetByQrCodeID")]
        public async Task<IActionResult> GetByQrCodeID(string qrCodeID)
        {
            var model =  await _service.GetByQRCodeID(qrCodeID);
            if(model.QrCode_Id != null)
                return Ok(model);
            else return NoContent();
        }
    }
}