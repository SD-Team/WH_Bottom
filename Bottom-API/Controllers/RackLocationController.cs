using System;
using System.Threading.Tasks;
using Bottom_API._Services.Interfaces;
using Bottom_API.DTO;
using Bottom_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Bottom_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RackLocationController : ControllerBase
    {
        private readonly IRackLocationService _service;
        public RackLocationController(IRackLocationService service)
        {
            _service = service;
        }

        [HttpPost(Name = "Filter")]
        public async Task<IActionResult> Filter([FromQuery]PaginationParams param, FilterRackLocationParam filterParam) 
        {
            var result = await _service.Filter(param, filterParam);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result);
        }

        [HttpPost("create", Name="Create")]
        public async Task<IActionResult> CreateBrand(RackLocation_Main_Dto rackDto)
        {
            rackDto.Updated_By = "Emma";
            if (await _service.Add(rackDto))
            {
                return CreatedAtRoute("Filter", new { });
            }

            throw new Exception("Creating the rack location failed on save");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(RackLocation_Main_Dto rackDto)
        {
            if (await _service.Update(rackDto))
                return NoContent();
            return BadRequest($"Updating rack {rackDto.Rack_Location} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (await _service.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the rack");
        }
    }
}