using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OutputController : ControllerBase
    {
        private readonly IOutputService _service;
        public OutputController(IOutputService service)
        {
            _service = service;
        }

        [HttpGet("GetByQrCodeId")]
        public async Task<IActionResult> GetByQrCodeId(string qrCodeId)
        {
            var model = await _service.GetByQrCodeId(qrCodeId);
            if (model != null)
                return Ok(model);
            else return NoContent();
        }

        [HttpPost("savelistoutput")]
        public async Task<IActionResult> SaveListOutput(List<OutputParam> outputParam)
        {
            if (await _service.SaveListOutput(outputParam))
            {
                return Ok();
            }

            throw new Exception("Submit failed on save");
        }
        
        [HttpGet("detail/{transacNo}")]
        public async Task<IActionResult> OutputDetail(string transacNo)
        {
            var model = await _service.GetDetailOutput(transacNo);
            if (model != null)
                return Ok(model);
            else return NoContent();
        }
    }
}