using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

namespace VtuberMerchHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ProducesResponseType(typeof(OrderReadDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] OrderCreateDTO dto)
        {
            var result = await _orderService.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(OrderReadDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return order is null ? NotFound() : Ok(order);
        }

        [Authorize]
        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
            => Ok(await _orderService.GetOrdersByCustomerIdAsync(customerId));

        [Authorize(Roles = "Vtuber")]
        [HttpGet("vtuber/{vtuberId:int}")]
        public async Task<IActionResult> GetByVtuber(int vtuberId)
        {
            var orders = await _orderService.GetOrdersByVtuberIdAsync(vtuberId);
            return Ok(orders);
        }

        [Authorize(Roles = "Admin,Vtuber")]
        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDTO dto)
        {
            var success = await _orderService.UpdateOrderStatusAsync(id, dto.Status);
            return success ? NoContent() : NotFound();
        }
    }
}