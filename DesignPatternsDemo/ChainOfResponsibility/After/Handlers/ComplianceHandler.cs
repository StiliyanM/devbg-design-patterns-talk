using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

public class ComplianceHandler : BasePaymentHandler, IComplianceHandler
{
    public override bool CanHandle(PaymentProcessingRequest request)
    {
        return request.RequiresComplianceCheck;
    }

    protected override void Process(PaymentProcessingRequest request, PaymentProcessingResult result)
    {
        result.ProcessingSteps.Add("Compliance Check - STARTED");

        if (string.IsNullOrWhiteSpace(request.MerchantId))
        {
            result.IsApproved = false;
            result.RejectionReason = "Compliance check failed: Merchant ID required";
            result.ProcessingSteps.Add("Compliance Check - FAILED");
            return;
        }

        if (request.Amount > 50000 && request.Country != "US")
        {
            result.IsApproved = false;
            result.RejectionReason = "Compliance check failed: Large international transaction requires additional verification";
            result.ProcessingSteps.Add("Compliance Check - FAILED");
            return;
        }

        result.ProcessingSteps.Add("Compliance Check - PASSED");
    }
}

