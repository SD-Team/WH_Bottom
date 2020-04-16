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

        [HttpGet("detail/{qrCode}", Name="GetDetail")]
        public async Task<IActionResult> GetDetailByQrCodeID(string qrCode)
        {
            var model = await _service.GetDetailByQRCodeID(qrCode);
            if (model != null)
                return Ok(model);
            else return NoContent();
        }

        [HttpPost("create", Name = "CreateInput")]
        public async Task<IActionResult> CreateInput(Transaction_Detail_Dto model)
        {
            if (await _service.CreateInput(model))
            {
                return Ok();
            }

            throw new Exception("Creating the rack location failed on save");
        }

        [HttpPut("submit", Name = "SubmitInput")]
        public async Task<IActionResult> SubmitInput(List<string> list)
        {
            if (await _service.SubmitInput(list))
            {
                return Ok();
            }

            throw new Exception("Submit failed on save");
        }

        [HttpGet("printmissing/{missingNo}", Name="PrintMissing")]
        public async Task<IActionResult> PrintMissing(string missingNo)
        {
            var model = await _service.GetMaterialPrint(missingNo);
            if (model != null)
                return Ok(model);
            else return NoContent();
        }

        [HttpPost("filterQrCodeAgain")]
        public async Task<IActionResult> FilterQrCodeAgain([FromQuery]PaginationParams param, FilterQrCodeAgainParam filterParam) 
        {
            var data = await _service.FilterQrCodeAgain(param,filterParam);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }

        [HttpGet("findMaterialName/{materialID}")]
        public async Task<IActionResult> FindMaterialName (string materialID) 
        {
            var materialName = await _service.FindMaterialName(materialID);
            return Ok(new {materialName = materialName});
        }
    }
}