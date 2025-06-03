using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.DTOs;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        // [Authorize]
        [HttpGet("merchandise/{merchandiseId}")]
        public async Task<IActionResult> GetProductsByMerchandise(int merchandiseId)
        {

            // var products = await _productService.GetProductsByMerchandiseIdAsync(merchandiseId);
            // return Ok(products);
            
            try
            {
                var products = await _productService.GetProductsByMerchandiseIdAsync(merchandiseId);
                var productDTOs = products.Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    MerchandiseId = p.MerchandiseId,
                    ProductName = p.ProductName,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Stock = p.Stock,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Category = p.Category == null ? null : new CategoryDTO
                    {
                        CategoryId = p.Category.CategoryId,
                        CategoryName = p.Category.CategoryName
                    }
                }).ToList();

                return Ok(productDTOs);
                // var products = await _productService.GetProductsByMerchandiseIdAsync(merchandiseId);
                // return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Vtuber,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
        {
            var createdProduct = await _productService.CreateProductAsync(
                request.MerchandiseId,
                request.ProductName,
                request.ImageFile,
                request.Price,
                request.Stock,
                request.Description,
                request.CategoryId
            );
            return Ok(createdProduct);
        }

        [Authorize(Roles = "Vtuber,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductRequest request)
        {
            var updatedProduct = await _productService.UpdateProductAsync(
                id,
                request.ProductName,
                request.ImageFile,
                request.Price,
                request.Stock,
                request.Description,
                request.CategoryId
            );
            return Ok(updatedProduct);
        }

        [Authorize(Roles = "Vtuber,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return Ok(new { Success = result });
        }
    }
}