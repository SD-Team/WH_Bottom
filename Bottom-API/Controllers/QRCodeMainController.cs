using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;
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

        [HttpPost("searchPlan")]
        public async Task<IActionResult> SearchByPlanNo([FromQuery]PaginationParams param, QRCodeSearchViewModel dataSearch) {
            var lists = await _service.SearchByPlanNo(param,dataSearch);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }

        [HttpGet("printqrcode/{qrCodeId}/version/{qrCodeVersion}", Name="PrintQrCode")]
        public async Task<IActionResult> PrintQrCode(string qrCodeId, int qrCodeVersion)
        {
            var model = await _service.GetQrCodePrint(qrCodeId, qrCodeVersion);
            if (model != null)
                return Ok(model);
            else return NoContent();
        }
    }
}