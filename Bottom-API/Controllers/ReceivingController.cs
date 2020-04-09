using System.Collections.Generic;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.Helpers;
using Bottom_API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceivingController : ControllerBase
    {
        private readonly IReceivingService _service;
        public ReceivingController(IReceivingService service)
        {
            _service = service;
        }
        
        [HttpPost("search")]
        public async Task<IActionResult> SearchByModel([FromBody]MaterialSearchViewModel model) {
            var lists = await _service.SearchByModel(model);
            return Ok(lists);
        }

        [HttpPost("searchTable")]
        public async Task<IActionResult> SearchByPurchase([FromBody]MaterialMainViewModel model) {
            var data = await _service.MaterialMerging(model);
            return Ok(data);   
        }

        [HttpPost("updateMaterial")]
        public async Task<IActionResult> UpdateMaterial([FromBody] List<OrderSizeByBatch> model) {
            var data  = await _service.UpdateMaterial(model);
            return Ok(data);
        }

        [HttpGet("receiveNoDetails/{receive_No}")]
        public async Task<IActionResult> ReceiveNoDetails(string receive_No) {
            var data = await _service.ReceiveNoDetails(receive_No);
            return Ok(data);
        }
        
        [HttpPost("purchaseNoDetail")]
        public async Task<IActionResult> PurchaseNoDetail([FromBody]MaterialMainViewModel model) {
            var data = await _service.PurchaseNoDetail(model);
            return Ok(data);
        }
    }
}