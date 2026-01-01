namespace DesignPatternsDemo.ChainOfResponsibility.Models;

public class PaymentProcessingRequest
{
    public string PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Country { get; set; }
    public string MerchantId { get; set; }
    public bool RequiresFraudCheck { get; set; }
    public bool RequiresComplianceCheck { get; set; }
}