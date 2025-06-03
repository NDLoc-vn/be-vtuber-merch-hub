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
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId);
        Task<Product> CreateProductAsync(int merchandiseId, string productName, IFormFile imageFile, decimal price, int? stock, string description, int? categoryId);
        Task<Product> UpdateProductAsync(int id, string productName, IFormFile imageFile, decimal price, int? stock, string description, int? categoryId);
        Task<bool> DeleteProductAsync(int id);
    }

    // ProductService
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(IProductRepository productRepository, ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id) ?? throw new Exception("Sản phẩm không tìm thấy");
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<List<Product>> GetProductsByMerchandiseIdAsync(int merchandiseId)
        {
            return await _productRepository.GetProductsByMerchandiseIdAsync(merchandiseId);
        }

        public async Task<Product> CreateProductAsync(int merchandiseId, string productName, IFormFile imageFile, decimal price, int? stock, string description, int? categoryId)
        {
            var product = new Product
            {
                MerchandiseId = merchandiseId,
                ProductName = productName,
                ImageUrl = await _cloudinaryService.UploadImageAsync(imageFile),
                Price = price,
                Stock = stock,
                Description = description,
                CategoryId = categoryId
            };
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(int id, string productName, IFormFile imageFile, decimal price, int? stock, string description, int? categoryId)
        {
            var product = await _productRepository.GetProductByIdAsync(id) ?? throw new Exception("Sản phẩm không tìm thấy");
            product.ProductName = productName ?? product.ProductName;
            product.Price = price != 0 ? price : product.Price;
            product.Stock = stock != 0 ? stock : product.Stock;
            product.Description = description ?? product.Description;
            product.CategoryId = categoryId ?? product.CategoryId;
            if (imageFile != null)
            {
                product.ImageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
            }
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }
    }
}