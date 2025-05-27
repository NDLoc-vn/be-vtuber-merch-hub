using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchandisesController : ControllerBase
    {
        private readonly IMerchandiseService _merchandiseService;

        public MerchandisesController(IMerchandiseService merchandiseService)
        {
            _merchandiseService = merchandiseService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllMerchandises()
        {
            var merchandises = await _merchandiseService.GetAllMerchandisesAsync();
            return Ok(merchandises);
        }

        // [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMerchandise(int id)
        {
            var merchandise = await _merchandiseService.GetMerchandiseByIdAsync(id);
            return Ok(merchandise);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPost]
        public async Task<IActionResult> CreateMerchandise([FromBody] CreateMerchandiseRequest merchandise)
        {
            var createdMerchandise = await _merchandiseService.CreateMerchandiseAsync(
                merchandise.VtuberId,
                merchandise.MerchandiseName,
                merchandise.ImageUrl,
                merchandise.StartDate,
                merchandise.EndDate,
                merchandise.Description
            );
            return Ok(createdMerchandise);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMerchandise(int id, [FromBody] UpdateMerchandiseRequest merchandise)
        {
            // var updatedMerchandise = await _merchandiseService.UpdateMerchandiseAsync(merchandise);
            var updatedMerchandise = await _merchandiseService.UpdateMerchandiseAsync(
                id,
                merchandise.MerchandiseName,
                merchandise.ImageUrl,
                merchandise.StartDate,
                merchandise.EndDate,
                merchandise.Description,
                merchandise.VtuberId
            );
            return Ok(updatedMerchandise);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMerchandise(int id)
        {
            var result = await _merchandiseService.DeleteMerchandiseAsync(id);
            return Ok(new { Success = result });
        }
    }
}