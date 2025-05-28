using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<CustomerDTO> GetCustomerInformationAsync(int userId);
    }

    // UserRepository
    public class UserRepository : IUserRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public UserRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CustomerDTO> GetCustomerInformationAsync(int userId)
        {
            return await _context.Customers
                .Include(c => c.Gender)
                .Include(c => c.User)
                .Where(c => c.UserId == userId)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    UserId = c.UserId,
                    FullName = c.FullName,
                    Nickname = c.Nickname,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    BirthDate = c.BirthDate,
                    Gender = c.Gender == null ? null : new GenderDTO
                    {
                        GenderId = c.Gender.GenderId,
                        GenderType = c.Gender.GenderType
                    },
                    User = new UserDTO
                    {
                        UserId = c.User.UserId,
                        Email = c.User.Email,
                        Role = c.User.Role,
                        AvatarUrl = c.User.AvatarUrl
                    }
                })
                .FirstOrDefaultAsync();
        }
    }
}