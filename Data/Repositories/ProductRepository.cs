using Microsoft.EntityFrameworkCore;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;

namespace VtuberMerchHub.Data
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }

    // ProductRepository
    public class ProductRepository : IProductRepository
    {
        private readonly VtuberMerchHubDbContext _context;

        public ProductRepository(VtuberMerchHubDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId)
        {
            return await _context.Products
                .Where(p => p.MerchandiseId == merchandiseId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}