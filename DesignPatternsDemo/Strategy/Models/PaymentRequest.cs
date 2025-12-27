namespace DesignPatternsDemo.Strategy.Models;

public class PaymentRequest
{
    public PaymentProvider Provider { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool IsRefund { get; set; }
}

public enum PaymentProvider
{
    Stripe,
    Adyen,
    LocalBank
}

