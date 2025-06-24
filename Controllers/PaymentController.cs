using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VtuberMerchHub.Controllers;
using VtuberMerchHub.Models;
using VtuberMerchHub.Services;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    public PaymentController(IConfiguration config, ICartService cartService, IOrderService orderService)
    {
        _config = config;
        _cartService = cartService;
        _orderService = orderService;
    }

    [Authorize(Roles = "Customer")]
    [HttpPost("vnpay")]
    public async Task<IActionResult> CreatePaymentUrl([FromBody] VNPayRequest req)
    {
        var cart = await _cartService.GetCartByCustomerIdAsync(req.CustomerId);
        if (cart == null || cart.CartItems.Count == 0)
            return BadRequest("Giỏ hàng trống");

        decimal total = cart.CartItems.Sum(i => i.Product.Price * i.Quantity);

        var tempId = Guid.NewGuid().ToString();

        // Lưu tạm đơn hàng
        TempOrderStore.Save(tempId, new TempOrder
        {
            CustomerId = req.CustomerId,
            ShippingAddress = req.ShippingAddress,
            Items = cart.CartItems.Select(i => new OrderItemDTO
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        });

        var config = _config.GetSection("VNPay").Get<VnPayConfig>();
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        var paymentUrl = VnPayHelper.GeneratePaymentUrl(config, total, tempId, ip);

        Console.WriteLine($"Generated payment URL: {paymentUrl}");

        return Ok(new { paymentUrl });
    }

    [HttpGet("vnpay-return")]
    public async Task<IActionResult> HandleReturn()
    {
        
        var config = _config.GetSection("VNPay").Get<VnPayConfig>();
        var query = Request.Query;

        Console.WriteLine("Received VNPay return query:");
        foreach (var (key, value) in query)
        {
            Console.WriteLine($"  {key}: {value}");
        }

        var vnp_SecureHash = query["vnp_SecureHash"];
        var inputData = query
            .Where(kvp => kvp.Key.StartsWith("vnp_") && kvp.Key != "vnp_SecureHash" && kvp.Key != "vnp_SecureHashType")
            .OrderBy(kvp => kvp.Key)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

        var signData = string.Join("&", inputData.Select(x => $"{x.Key}={x.Value}"));
        Console.WriteLine("SIGN_DATA RETURNED = " + signData);
        var computedHash = VnPayHelper.HmacSHA512(config.HashSecret, signData);

        Console.WriteLine($"Computed Hash: {computedHash}");

        if (computedHash != vnp_SecureHash)
            return BadRequest("Chữ ký không hợp lệ");

        var tempId = inputData["vnp_TxnRef"];
        var responseCode = inputData["vnp_ResponseCode"];

        if (responseCode != "00")
            return Redirect("https://fe-vtubermerchhub.vercel.app/order?status=fail");

        var temp = TempOrderStore.Get(tempId);
        if (temp == null) return BadRequest("Không tìm thấy dữ liệu đơn hàng");

        TempOrderStore.Remove(tempId);

        var orderDto = new OrderCreateDTO
        {
            CustomerId = temp.CustomerId,
            ShippingAddress = temp.ShippingAddress,
            Items = temp.Items.Select(i => new OrderItemCreateDTO
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        await _orderService.CreateOrderAsync(orderDto);

        return Redirect("https://fe-vtubermerchhub.vercel.app/order?status=success");
    }
}

public class VNPayRequest
{
    public int CustomerId { get; set; }
    public string ShippingAddress { get; set; }
}



public class PaymentRequest
{
    public int CustomerId { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItemDTO> Items { get; set; } = new();
}

public class OrderItemDTO
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class TempOrder
{
    public int CustomerId { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItemDTO> Items { get; set; }
}

public static class TempOrderStore
{
    private static readonly Dictionary<string, TempOrder> _orders = new();

    public static void Save(string key, TempOrder order) => _orders[key] = order;

    public static TempOrder? Get(string key) =>
        _orders.TryGetValue(key, out var value) ? value : null;

    public static void Remove(string key) => _orders.Remove(key);
}
