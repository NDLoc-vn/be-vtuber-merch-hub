using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    public interface IMerchandiseRepository
    {
        Task<Merchandise> GetMerchandiseByIdAsync(int id);
        Task<List<Merchandise>> GetAllMerchandisesAsync();
        Task<Merchandise> CreateMerchandiseAsync(Merchandise merchandise);
        Task<Merchandise> UpdateMerchandiseAsync(Merchandise merchandise);
        Task<bool> DeleteMerchandiseAsync(int id);
    }

    // MerchandiseRepository
    public class MerchandiseRepository : IMerchandiseRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public MerchandiseRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<Merchandise> GetMerchandiseByIdAsync(int id)
        {
            return await _context.Merchandises
                .Include(m => m.Vtuber)
                .Include(m => m.Products)
                .FirstOrDefaultAsync(m => m.MerchandiseId == id);
        }

        public async Task<List<Merchandise>> GetAllMerchandisesAsync()
        {
            return await _context.Merchandises.ToListAsync();
        }

        public async Task<Merchandise> CreateMerchandiseAsync(Merchandise merchandise)
        {
            _context.Merchandises.Add(merchandise);
            await _context.SaveChangesAsync();
            return merchandise;
        }

        public async Task<Merchandise> UpdateMerchandiseAsync(Merchandise merchandise)
        {
            _context.Merchandises.Update(merchandise);
            await _context.SaveChangesAsync();
            return merchandise;
        }

        public async Task<bool> DeleteMerchandiseAsync(int id)
        {
            var merchandise = await _context.Merchandises.FindAsync(id);
            if (merchandise == null) return false;
            _context.Merchandises.Remove(merchandise);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}