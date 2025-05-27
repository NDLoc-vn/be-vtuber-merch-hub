using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VtubersController : ControllerBase
    {
        private readonly IVtuberService _vtuberService;

        public VtubersController(IVtuberService vtuberService)
        {
            _vtuberService = vtuberService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllVtubers()
        {
            var vtubers = await _vtuberService.GetAllVtubersAsync();
            return Ok(vtubers);
        }

        // [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVtuber(int id)
        {
            var vtuber = await _vtuberService.GetVtuberByIdAsync(id);
            return Ok(vtuber);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPost]
        public async Task<IActionResult> CreateVtuber([FromForm] CreateVtuberRequest request)
        {
            var createdVtuber = await _vtuberService.CreateVtuberAsync(
                request.UserId,
                request.VtuberName,
                request.RealName,
                request.DebutDate,
                request.Channel,
                request.Description,
                request.VtuberGender,
                request.SpeciesId,
                request.CompanyId,
                request.ModelFile
            );
            return Ok(createdVtuber);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVtuber(int id, [FromForm] UpdateVtuberRequest request)
        {
            var updatedVtuber = await _vtuberService.UpdateVtuberAsync(
                id,
                request.VtuberName,
                request.RealName,
                request.DebutDate,
                request.Channel,
                request.Description,
                request.VtuberGender,
                request.SpeciesId,
                request.CompanyId,
                request.ModelFile
            );
            return Ok(updatedVtuber);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVtuber(int id)
        {
            var result = await _vtuberService.DeleteVtuberAsync(id);
            return Ok(new { Success = result });
        }
    }
}