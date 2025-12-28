using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

public class FraudCheckHandler : BasePaymentHandler, IFraudCheckHandler
{
    public override bool CanHandle(PaymentProcessingRequest request)
    {
        return request.RequiresFraudCheck;
    }

    protected override void Process(PaymentProcessingRequest request, PaymentProcessingResult result)
    {
        result.ProcessingSteps.Add("Fraud Check - STARTED");

        if (request.Amount > 10000)
        {
            result.IsApproved = false;
            result.RejectionReason = "Fraud check failed: Amount exceeds threshold";
            result.ProcessingSteps.Add("Fraud Check - FAILED");
            return;
        }

        if (request.Country is "CU" or "IR" or "KP" or "SY")
        {
            result.IsApproved = false;
            result.RejectionReason = "Fraud check failed: Blocked country";
            result.ProcessingSteps.Add("Fraud Check - FAILED");
            return;
        }

        result.ProcessingSteps.Add("Fraud Check - PASSED");
    }
}

