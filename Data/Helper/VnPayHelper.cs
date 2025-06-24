using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

public static class VnPayHelper
{
    public static string GeneratePaymentUrl(VnPayConfig config, decimal amount, string orderId, string ipAddress)
    {
        var vnp_Amount = ((int)(amount * 100)).ToString();
        var vnp_TxnRef = orderId;
        var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");

        var inputData = new SortedDictionary<string, string>
        {
            { "vnp_Version", "2.1.0" },
            { "vnp_Command", "pay" },
            { "vnp_TmnCode", config.TmnCode },
            { "vnp_Amount", vnp_Amount },
            { "vnp_CurrCode", "VND" },
            { "vnp_TxnRef", vnp_TxnRef },
            { "vnp_OrderInfo", $"Thanh toan don hang {orderId}" },
            { "vnp_OrderType", "other" },
            { "vnp_Locale", "vn" },
            { "vnp_ReturnUrl", config.ReturnUrl },
            { "vnp_IpAddr", ipAddress },
            { "vnp_CreateDate", vnp_CreateDate }
        };

        var signData = string.Join('&', inputData.Select(x => $"{x.Key}={x.Value}"));
        Console.WriteLine("SIGN_DATA SENT = " + signData);
        var hash = HmacSHA512(config.HashSecret, signData);

        inputData.Add("vnp_SecureHash", hash);

        return QueryHelpers.AddQueryString(config.BaseUrl, inputData);
    }

    public static string HmacSHA512(string key, string data)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
    }
}
