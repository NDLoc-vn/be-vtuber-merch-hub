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

        [Authorize]
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
    }
}