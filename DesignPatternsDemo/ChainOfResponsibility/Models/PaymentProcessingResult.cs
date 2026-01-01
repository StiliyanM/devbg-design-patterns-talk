namespace DesignPatternsDemo.ChainOfResponsibility.Models;

public class PaymentProcessingResult
{
	public bool IsApproved { get; set; }
	public string RejectionReason { get; set; }
	public decimal CalculatedFee { get; set; }
	public List<string> ProcessingSteps { get; set; } = [];
}