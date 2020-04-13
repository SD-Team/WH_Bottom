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

        [HttpPost("submit", Name = "SubmitTransfer")]
        public async Task<IActionResult> Submit(List<TransferLocation_Dto> lists)
        {
            if (await _service.SubmitTransfer(lists))
            {
                return Ok();
            }

            throw new Exception("Submit failed on save");
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromQuery]PaginationParams paginationParams, TransferLocationParam transferLocationParam) {
            var result = await _service.Search(transferLocationParam, paginationParams);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result);
        }

        [HttpGet("GetDetailTransaction")]
        public async Task<IActionResult> GetDetailTransaction(string transferNo) {
            var result = await _service.GetDetailTransaction(transferNo);
            return Ok(result);
        }
    }
}