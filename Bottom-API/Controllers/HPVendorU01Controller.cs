using System.Linq;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HPVendorU01Controller : ControllerBase
    {
        private readonly IHPVendorU01Service _service;
        public HPVendorU01Controller(IHPVendorU01Service service) {
            _service = service;
        }

        [HttpGet("all", Name = "GetAll")]
        public async Task<IActionResult> GetAlls() {
            var lists = await _service.GetAllAsync();
            return Ok(lists);
        }
    }
}