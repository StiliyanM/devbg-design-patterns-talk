namespace DesignPatternsDemo.Strategy.Models;

public class PaymentRequest
{
    public PaymentProvider Provider { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Country { get; set; }
    public bool IsRefund { get; set; }
}