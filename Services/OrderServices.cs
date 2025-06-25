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
using VtuberMerchHub.Controllers;
using Microsoft.EntityFrameworkCore;

namespace VtuberMerchHub.Services
{
    public interface IOrderService
    {
        Task<OrderReadDTO> CreateOrderAsync(OrderCreateDTO dto);
        Task<OrderReadDTO?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderReadDTO>> GetOrdersByVtuberIdAsync(int vtuberId);
        Task<IEnumerable<OrderReadDTO>> GetOrdersByCustomerIdAsync(int customerId);
        Task<bool> UpdateOrderStatusAsync(int id, int status);
    }

    // OrderService
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly VtuberMerchHubDbContext _ctx;

        public OrderService(IOrderRepository orderRepository, VtuberMerchHubDbContext ctx)
        {
            _orderRepository = orderRepository;
            _ctx = ctx;
        }

        public async Task<OrderReadDTO> CreateOrderAsync(OrderCreateDTO dto)
        {
            // 1) Lấy thông tin sản phẩm & giá hiện tại
            var productIds = dto.Items.Select(i => i.ProductId).Distinct();
            var products = await _ctx.Products
                                    .Where(p => productIds.Contains(p.ProductId))
                                    .ToListAsync();

            // 2) Kiểm tra tồn kho, tính tổng
            decimal total = 0;
            var details = new List<OrderDetail>();
            foreach (var item in dto.Items)
            {
                var product = products.FirstOrDefault(p => p.ProductId == item.ProductId)
                            ?? throw new Exception($"Product {item.ProductId} not found");

                // if (product.Stock < item.Quantity)
                //     throw new Exception($"Product {product.ProductName} is out of stock");

                total += product.Price * item.Quantity;

                details.Add(new OrderDetail
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });

                product.Stock -= item.Quantity; // trừ kho
            }

            // 3) Tạo Order
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                ShippingAddress = dto.ShippingAddress,
                OrderDate = DateTime.UtcNow.AddHours(7),
                TotalAmount = total,
                Status = 0,
                OrderDetails = details
            };

            var saved = await _orderRepository.CreateOrderAsync(order);

            return MapToReadDTO(saved);
        }

        public async Task<OrderReadDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return order is null ? null : MapToReadDTO(order);
        }

        public async Task<IEnumerable<OrderReadDTO>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var list = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            return list.Select(MapToReadDTO);
        }
        
        public async Task<IEnumerable<OrderReadDTO>> GetOrdersByVtuberIdAsync(int vtuberId)
        {
            var orders = await _orderRepository.GetOrdersByVtuberIdAsync(vtuberId);
            foreach (var order in orders)
            {
                order.OrderDetails = order.OrderDetails
                    .Where(od => od.Product?.Merchandise?.VtuberId == vtuberId)
                    .ToList();
            }
            return orders.Select(MapToReadDTO);
        }
        
        public async Task<bool> UpdateOrderStatusAsync(int id, int status)
        {
            var order = await _ctx.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return false;

            order.Status = status;
            await _ctx.SaveChangesAsync();
            return true;
        }
        
        /* ---------- Helpers ---------- */
        private static OrderReadDTO MapToReadDTO(Order o) => new()
        {
            OrderId = o.OrderId,
            CustomerId = o.CustomerId,
            Customer = o.Customer is null ? null : new CustomerBriefDTO
            {
                CustomerId = o.Customer.CustomerId,
                FullName = o.Customer.FullName,
                Nickname = o.Customer.Nickname ?? string.Empty,
                PhoneNumber = o.Customer.PhoneNumber ?? string.Empty,
            },
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            ShippingAddress = o.ShippingAddress,
            Status = o.Status,
            Items = o.OrderDetails.Select(d => new OrderItemReadDTO
            {
                ProductId = d.ProductId,
                Name = d.Product?.ProductName ?? string.Empty,
                ImageUrl = d.Product?.ImageUrl ?? string.Empty,
                Price = d.Price,
                Quantity = d.Quantity,
                MerchandiseId = d.Product?.MerchandiseId ?? 0,
                MerchandiseName = d.Product?.Merchandise?.MerchandiseName ?? string.Empty
            }).ToList()
        };
    }
}