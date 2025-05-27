using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    public interface IVtuberRepository
    {
        Task<VtuberDTO> GetVtuberByIdAsync(int id);
        Task<List<VtuberDTO>> GetAllVtubersAsync();
        Task<Vtuber> CreateVtuberAsync(Vtuber vtuber);
        Task<Vtuber> UpdateVtuberAsync(Vtuber vtuber);
        Task<bool> DeleteVtuberAsync(int id);
    }

    // VtuberRepository
    public class VtuberRepository : IVtuberRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public VtuberRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<VtuberDTO> GetVtuberByIdAsync(int id)
        {
            var merchandise = await _context.Vtubers
                .Include(v => v.User)
                .Include(v => v.Gender)
                .Include(v => v.Species)
                .Include(v => v.Company)
                .Include(v => v.Merchandises)
                    .ThenInclude(m => m.Products)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(v => v.VtuberId == id);
            if (merchandise == null) return null;

            var dto = new VtuberDTO
            {
                VtuberId = merchandise.VtuberId,
                UserId = merchandise.UserId,
                VtuberName = merchandise.VtuberName,
                RealName = merchandise.RealName,
                DebutDate = merchandise.DebutDate,
                Channel = merchandise.Channel,
                Description = merchandise.Description,
                VtuberGender = merchandise.VtuberGender,
                SpeciesId = merchandise.SpeciesId,
                CompanyId = merchandise.CompanyId,
                ModelUrl = merchandise.ModelUrl,
                User = merchandise.User == null ? null : new UserDTO
                {
                    UserId = merchandise.User.UserId,
                    Email = merchandise.User.Email,
                    Role = merchandise.User.Role,
                    CreatedAt = merchandise.User.CreatedAt,
                    UpdatedAt = merchandise.User.UpdatedAt,
                    AvatarUrl = merchandise.User.AvatarUrl
                },
                Gender = merchandise.Gender == null ? null : new GenderDTO
                {
                    GenderId = merchandise.Gender.GenderId,
                    GenderType = merchandise.Gender.GenderType
                },
                Species = merchandise.Species == null ? null : new SpeciesDTO
                {
                    SpeciesId = merchandise.Species.SpeciesId,
                    SpeciesName = merchandise.Species.SpeciesName,
                    Description = merchandise.Species.Description
                },
                Company = merchandise.Company == null ? null : new CompanyDTO
                {
                    CompanyId = merchandise.Company.CompanyId,
                    CompanyName = merchandise.Company.CompanyName,
                    Address = merchandise.Company.Address,
                    ContactEmail = merchandise.Company.ContactEmail
                },
                Merchandises = merchandise.Merchandises?.Select(m => new MerchandiseDTO
                {
                    MerchandiseId = m.MerchandiseId,
                    VtuberId = m.VtuberId,
                    MerchandiseName = m.MerchandiseName,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Products = m.Products?.Select(p => new ProductDTO
                    {
                        ProductId = p.ProductId,
                        MerchandiseId = p.MerchandiseId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Stock = p.Stock,
                        ImageUrl = p.ImageUrl,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        Category = p.Category == null ? null : new CategoryDTO
                        {
                            CategoryId = p.Category.CategoryId,
                            CategoryName = p.Category.CategoryName,
                            Description = p.Category.Description
                        }
                    }).ToList() ?? new List<ProductDTO>()
                }).ToList() ?? new List<MerchandiseDTO>()
            };
            return dto;
        }

        public async Task<List<VtuberDTO>> GetAllVtubersAsync()
        {
            var vtuberEntities = await _context.Vtubers
                .Include(v => v.User)
                .Include(v => v.Gender)
                .Include(v => v.Species)
                .Include(v => v.Company)
                .Include(v => v.Merchandises)
                    .ThenInclude(m => m.Products)
                        .ThenInclude(p => p.Category)
                .ToListAsync();

            return vtuberEntities.Select(v => new VtuberDTO
            {
                VtuberId = v.VtuberId,
                UserId = v.UserId,
                VtuberName = v.VtuberName,
                RealName = v.RealName,
                DebutDate = v.DebutDate,
                Channel = v.Channel,
                Description = v.Description,
                VtuberGender = v.VtuberGender,
                SpeciesId = v.SpeciesId,
                CompanyId = v.CompanyId,
                ModelUrl = v.ModelUrl,
                User = v.User == null ? null : new UserDTO
                {
                    UserId = v.User.UserId,
                    Email = v.User.Email,
                    Role = v.User.Role,
                    CreatedAt = v.User.CreatedAt,
                    UpdatedAt = v.User.UpdatedAt,
                    AvatarUrl = v.User.AvatarUrl
                },
                Gender = v.Gender == null ? null : new GenderDTO
                {
                    GenderId = v.Gender.GenderId,
                    GenderType = v.Gender.GenderType
                },
                Species = v.Species == null ? null : new SpeciesDTO
                {
                    SpeciesId = v.Species.SpeciesId,
                    SpeciesName = v.Species.SpeciesName,
                    Description = v.Species.Description
                },
                Company = v.Company == null ? null : new CompanyDTO
                {
                    CompanyId = v.Company.CompanyId,
                    CompanyName = v.Company.CompanyName,
                    Address = v.Company.Address,
                    ContactEmail = v.Company.ContactEmail
                },
                Merchandises = v.Merchandises?.Select(m => new MerchandiseDTO
                {
                    MerchandiseId = m.MerchandiseId,
                    VtuberId = m.VtuberId,
                    MerchandiseName = m.MerchandiseName,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Products = m.Products?.Select(p => new ProductDTO
                    {
                        ProductId = p.ProductId,
                        MerchandiseId = p.MerchandiseId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Stock = p.Stock,
                        ImageUrl = p.ImageUrl,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        Category = p.Category == null ? null : new CategoryDTO
                        {
                            CategoryId = p.Category.CategoryId,
                            CategoryName = p.Category.CategoryName,
                            Description = p.Category.Description
                        }
                    }).ToList() ?? new List<ProductDTO>()
                }).ToList() ?? new List<MerchandiseDTO>()
            }).ToList();
        }

        public async Task<Vtuber> CreateVtuberAsync(Vtuber vtuber)
        {
            _context.Vtubers.Add(vtuber);
            await _context.SaveChangesAsync();
            return vtuber;
        }

        public async Task<Vtuber> UpdateVtuberAsync(Vtuber vtuber)
        {
            _context.Vtubers.Update(vtuber);
            await _context.SaveChangesAsync();
            return vtuber;
        }

        public async Task<bool> DeleteVtuberAsync(int id)
        {
            var vtuber = await _context.Vtubers.FindAsync(id);
            if (vtuber == null) return false;
            _context.Vtubers.Remove(vtuber);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}