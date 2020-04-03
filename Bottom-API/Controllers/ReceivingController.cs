using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.Helpers;
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


    }
}