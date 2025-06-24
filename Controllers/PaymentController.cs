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
    private readonly IOrderService _orderService;

    public PaymentController(IConfiguration config, IOrderService orderService)
    {
        _config = config;
        _orderService = orderService;
    }

    [HttpPost("vnpay")]
    public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentRequest req)
    {
        if (req.Items.Count == 0) return BadRequest("Giỏ hàng trống");

        // Tạm tính totalAmount
        decimal total = 0;
        foreach (var item in req.Items)
        {
            // Tối giản: bạn có thể thêm validation giá thật nếu cần
            total += 100000 * item.Quantity; // TODO: thay thế bằng giá thật từ DB
        }

        var tempId = Guid.NewGuid().ToString(); // mã tạm cho đơn hàng
        TempOrderStore.Save(tempId, new TempOrder
        {
            CustomerId = req.CustomerId,
            ShippingAddress = req.ShippingAddress,
            Items = req.Items
        });

        var config = _config.GetSection("VNPay").Get<VnPayConfig>();
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
        var paymentUrl = VnPayHelper.GeneratePaymentUrl(config, total, tempId, ip);

        return Ok(new { paymentUrl });
    }

    [HttpGet("vnpay-return")]
    public async Task<IActionResult> HandleReturn()
    {
        var config = _config.GetSection("VNPay").Get<VnPayConfig>();
        var query = Request.Query;

        var vnp_SecureHash = query["vnp_SecureHash"];
        var inputData = query
            .Where(kvp => kvp.Key.StartsWith("vnp_") && kvp.Key != "vnp_SecureHash" && kvp.Key != "vnp_SecureHashType")
            .OrderBy(kvp => kvp.Key)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

        var signData = string.Join("&", inputData.Select(x => $"{x.Key}={x.Value}"));
        var computedHash = VnPayHelper.HmacSHA512(config.HashSecret, signData);

        Console.WriteLine("SIGN_DATA = " + signData);
        Console.WriteLine("HASH_SECRET = " + config.HashSecret);
        Console.WriteLine("COMPUTED_HASH = " + computedHash);
        Console.WriteLine("RECEIVED_HASH = " + vnp_SecureHash);


        if (computedHash != vnp_SecureHash)
            return BadRequest("Chữ ký không hợp lệ");

        var tempId = inputData["vnp_TxnRef"];
        var responseCode = inputData["vnp_ResponseCode"];

        if (responseCode != "00")
            return Redirect("/failed");

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

        return Redirect("/success");
    }
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
