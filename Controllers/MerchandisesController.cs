using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> GetAllMerchandises()
        {
            var merchandises = await _merchandiseService.GetAllMerchandisesAsync();
            return Ok(merchandises);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetMerchandisesByUserId(int userId)
        {
            var merchandises = await _merchandiseService.GetMerchandisesByUserIdAsync(userId);
            return Ok(merchandises);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMerchandise(int id)
        {
            var merchandise = await _merchandiseService.GetMerchandiseByIdAsync(id);
            return Ok(merchandise);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPost]
        public async Task<IActionResult> CreateMerchandise([FromForm] CreateMerchandiseRequest request)
        {
            var created = await _merchandiseService.CreateMerchandiseAsync(
                request.VtuberId,
                request.MerchandiseName,
                request.ImageUrl,
                request.StartDate,
                request.EndDate,
                request.Description
            );
            return Ok(created);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMerchandise(int id, [FromForm] UpdateMerchandiseRequest request)
        {
            var updated = await _merchandiseService.UpdateMerchandiseAsync(
                id,
                request.MerchandiseName,
                request.ImageUrl,
                request.StartDate,
                request.EndDate,
                request.Description,
                request.VtuberId
            );
            return Ok(updated);
        }

        [Authorize(Roles = "Vtuber")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMerchandise(int id)
        {
            var result = await _merchandiseService.DeleteMerchandiseAsync(id);
            return Ok(new { success = result });
        }
    }
}
