using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    public interface IMerchandiseRepository
    {
        Task<List<MerchandiseDTO>> GetAllMerchandisesAsync();
        Task<List<MerchandiseDTO>> GetMerchandisesByUserIdAsync(int userId);
        Task<MerchandiseDTO?> GetMerchandiseByIdAsync(int id);
        Task<Merchandise?> FindEntityByIdAsync(int id);
        Task<MerchandiseDTO> CreateMerchandiseAsync(Merchandise merchandise);
        Task<MerchandiseDTO> UpdateMerchandiseAsync(Merchandise merchandise);
        Task<bool> DeleteMerchandiseAsync(int id);
    }

    public class MerchandiseRepository : IMerchandiseRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public MerchandiseRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        private static MerchandiseDTO MapToDTO(Merchandise m) => new MerchandiseDTO
        {
            MerchandiseId = m.MerchandiseId,
            VtuberId = m.VtuberId,
            MerchandiseName = m.MerchandiseName,
            ImageUrl = m.ImageUrl,
            Description = m.Description,
            StartDate = m.StartDate,
            EndDate = m.EndDate,
            Vtuber = m.Vtuber != null ? new VtuberDTO
            {
                VtuberId = m.Vtuber.VtuberId,
                UserId = m.Vtuber.UserId,
                VtuberName = m.Vtuber.VtuberName,
                RealName = m.Vtuber.RealName,
                DebutDate = m.Vtuber.DebutDate,
                Channel = m.Vtuber.Channel,
                Description = m.Vtuber.Description,
                VtuberGender = m.Vtuber.VtuberGender,
                SpeciesId = m.Vtuber.SpeciesId,
                CompanyId = m.Vtuber.CompanyId,
                ModelUrl = m.Vtuber.ModelUrl,
            } : null,
            Products = m.Products?.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                MerchandiseId = p.MerchandiseId,
                ProductName = p.ProductName,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Stock = p.Stock,
                Description = p.Description,
                CategoryId = p.CategoryId,
                Category = p.Category != null ? new CategoryDTO
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    Description = p.Category.Description
                } : null
            }).ToList() ?? new List<ProductDTO>()
        };

        public async Task<List<MerchandiseDTO>> GetAllMerchandisesAsync()
        {
            var list = await _context.Merchandises
                .Include(m => m.Vtuber)
                .Include(m => m.Products)
                .ThenInclude(p => p.Category)
                .ToListAsync();

            return list.Select(MapToDTO).ToList();
        }

        public async Task<List<MerchandiseDTO>> GetMerchandisesByUserIdAsync(int userId)
        {
            var merchandises = await _context.Merchandises
                .Include(m => m.Products)
                    .ThenInclude(p => p.Category)
                .Include(m => m.Vtuber)
                .Where(m => m.Vtuber != null && m.Vtuber.UserId == userId)
                .ToListAsync();

            return merchandises.Select(MapToDTO).ToList();
        }

        public async Task<MerchandiseDTO?> GetMerchandiseByIdAsync(int id)
        {
            var merchandise = await _context.Merchandises
                .Include(m => m.Vtuber)
                .Include(m => m.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(m => m.MerchandiseId == id);

            return merchandise == null ? null : MapToDTO(merchandise);
        }

        public async Task<Merchandise?> FindEntityByIdAsync(int id)
        {
            return await _context.Merchandises.FindAsync(id);
        }

        public async Task<MerchandiseDTO> CreateMerchandiseAsync(Merchandise merchandise)
        {
            _context.Merchandises.Add(merchandise);
            await _context.SaveChangesAsync();
            return MapToDTO(merchandise);
        }

        public async Task<MerchandiseDTO> UpdateMerchandiseAsync(Merchandise merchandise)
        {
            _context.Merchandises.Update(merchandise);
            await _context.SaveChangesAsync();
            return MapToDTO(merchandise);
        }

        public async Task<bool> DeleteMerchandiseAsync(int id)
        {
            var entity = await _context.Merchandises.FindAsync(id);
            if (entity == null) return false;
            _context.Merchandises.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
