using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUserService _userService;

        public CustomersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize (Roles = "Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerInformation(int id)
        {
            var customer = await _userService.GetCustomerInformationAsync(id);
            if (customer == null)
            {
                return NotFound(new { Message = "Khách hàng không tồn tại" });
            }
            return Ok(customer);
        }

        [Authorize (Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerInformation(int id, [FromForm] UpdateCustomerRequest request)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID không hợp lệ" });
            }

            var updatedCustomer = await _userService.UpdateCustomerAsync(
                id,
                request.Avatar,
                request.Address,
                request.PhoneNumber,
                request.FullName,
                request.Nickname,
                request.BirthDate,
                request.GenderId
            );
            if (updatedCustomer == null)
            {
                return NotFound(new { Message = "Khách hàng không tồn tại" });
            }
            return Ok(updatedCustomer);
        }
    }
}