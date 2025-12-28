using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

public class FeeCalculationHandler : BasePaymentHandler, IFeeCalculationHandler
{
    public override bool CanHandle(PaymentProcessingRequest request) => true; // Always runs

    protected override void Process(PaymentProcessingRequest request, PaymentProcessingResult result)
    {
        result.ProcessingSteps.Add("Fee Calculation - STARTED");

        // Simple fee calculation: 2.5% + $0.30
        var percentageFee = request.Amount * 0.025m;
        const decimal fixedFee = 0.30m;
        result.CalculatedFee = Math.Round(percentageFee + fixedFee, 2);

        result.ProcessingSteps.Add($"Fee Calculation - ${result.CalculatedFee:F2}");
    }
}

