using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await _userService.RegisterUserAsync(request.Email, request.Password, request.Role);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.LoginUserAsync(request.Email, request.Password);
            var role = await _userService.FindRoleUserAsync(request.Email, request.Password);
            if (role == null)
            {
                return Unauthorized(new { Message = "Email hoặc mật khẩu không đúng" });
            }
            return Ok(new { Token = token, Role = role });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _userService.ForgotPasswordAsync(request.Email);
            return Ok(new { Message = "Mật khẩu mới đã được gửi (giả lập)" });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UpdateUserRequest request)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request.Email, request.Avatar);
            return Ok(updatedUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(new { Success = result });
        }
    }
}