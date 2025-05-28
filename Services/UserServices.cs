using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VtuberMerchHub.Data;
using VtuberMerchHub.Models;
using Microsoft.AspNetCore.Http;
using VtuberMerchHub.DTOs;

namespace VtuberMerchHub.Services
{
    public interface IUserService
    {
        Task<UserDTO> RegisterUserAsync(string email, string password, string role, string name);
        Task<string> LoginUserAsync(string email, string password);
        Task<string> FindRoleUserAsync(string email, string password);
        Task<int> FindIdUserAsync(string email, string password);
        Task<bool> ForgotPasswordAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<CustomerDTO> GetCustomerInformationAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(int id, string email, IFormFile avatar);
        Task<CustomerDTO> UpdateCustomerAsync(int id, IFormFile? avatar, string? address, string? phoneNumber, string? fullName, string? nickname, DateTime? birthDate, int? genderId);
        Task<bool> DeleteUserAsync(int id);
    }

    // UserService
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ICloudinaryService _cloudinaryService;

        public UserService(IUserRepository userRepository, IConfiguration configuration, ICloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<UserDTO> RegisterUserAsync(string email, string password, string role, string name)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
                throw new Exception("Người dùng đã tồn tại");

            var user = new User
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role
            };

            var createdUser = await _userRepository.CreateUserAsync(user);
            if (role == "Customer")
            {
                // Create a customer profile if the user is a customer
                var customerDto = new CustomerDTO
                {
                    UserId = createdUser.UserId,
                    FullName = name,
                    Nickname = name,
                    Address = string.Empty,
                    PhoneNumber = string.Empty,
                    BirthDate = null,
                    GenderId = null
                };
                await _userRepository.CreateCustomerAsync(customerDto);
            }
            return createdUser;
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new Exception("Email không tồn tại");

            Console.WriteLine($"Hashed Password: {user.Password}");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Mật khẩu không đúng");

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiryInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> FindRoleUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new Exception("Người dùng không tồn tại");

            return user.Role;
        }

        public async Task<int> FindIdUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new Exception("Người dùng không tồn tại");

            return user.UserId;
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new Exception("Email không tồn tại");

            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id) ?? throw new Exception("Người dùng không tìm thấy");
        }

        public async Task<CustomerDTO> GetCustomerInformationAsync(int id)
        {
            var customer = await _userRepository.GetCustomerInformationAsync(id);
            if (customer == null)
                throw new Exception("Người dùng không tìm thấy");

            return customer;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> UpdateUserAsync(int id, string email, IFormFile avatar)
        {
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new Exception("Người dùng không tìm thấy");
            user.Email = email ?? user.Email;
            if (avatar != null)
            {
                user.AvatarUrl = await _cloudinaryService.UploadImageAsync(avatar);
            }
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(int id, IFormFile? avatar, string? address, string? phoneNumber, string? fullName, string? nickname, DateTime? birthDate, int? genderId)
        {
            var customer = await _userRepository.GetCustomerInformationAsync(id) ?? throw new Exception("Người dùng không tìm thấy");
            if (avatar != null)
            {
                customer.User.AvatarUrl = await _cloudinaryService.UploadImageAsync(avatar);
            }
            customer.Address = address ?? customer.Address;
            customer.PhoneNumber = phoneNumber ?? customer.PhoneNumber;
            customer.FullName = fullName ?? customer.FullName;
            customer.Nickname = nickname ?? customer.Nickname;
            customer.BirthDate = birthDate ?? customer.BirthDate;
            if (genderId.HasValue)
            {
                customer.GenderId = genderId.Value;
            }
            if (genderId.HasValue && genderId.Value <= 0)
            {
                throw new ArgumentException("GenderId must be a positive integer");
            }
            return await _userRepository.UpdateCustomerAsync(customer);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}