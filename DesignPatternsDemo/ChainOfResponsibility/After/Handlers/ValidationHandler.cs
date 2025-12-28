using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

public class ValidationHandler : BasePaymentHandler, IValidationHandler
{
    public override bool CanHandle(PaymentProcessingRequest request) => true; // Always runs

    protected override void Process(PaymentProcessingRequest request, PaymentProcessingResult result)
    {
        result.ProcessingSteps.Add("Basic Validation - STARTED");

        if (string.IsNullOrWhiteSpace(request.PaymentId))
        {
            result.IsApproved = false;
            result.RejectionReason = "Payment ID is required";
            result.ProcessingSteps.Add("Basic Validation - FAILED");
            return;
        }

        if (request.Amount <= 0)
        {
            result.IsApproved = false;
            result.RejectionReason = "Amount must be greater than zero";
            result.ProcessingSteps.Add("Basic Validation - FAILED");
            return;
        }

        if (string.IsNullOrWhiteSpace(request.Currency))
        {
            result.IsApproved = false;
            result.RejectionReason = "Currency is required";
            result.ProcessingSteps.Add("Basic Validation - FAILED");
            return;
        }

        result.ProcessingSteps.Add("Basic Validation - PASSED");
    }
}

