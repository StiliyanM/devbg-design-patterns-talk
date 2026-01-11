using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.Before;

public class PaymentProcessor
{
    public PaymentProcessingResult ProcessPayment(PaymentProcessingRequest request)
    {
        var result = new PaymentProcessingResult
        {
            ProcessingSteps = []
        };

        // Step 1: Basic Validation
        if (string.IsNullOrWhiteSpace(request.PaymentId))
        {
            result.IsApproved = false;
            result.RejectionReason = "Payment ID is required";
            result.ProcessingSteps.Add("Basic Validation - FAILED");
            return result;
        }

        if (request.Amount <= 0)
        {
            result.IsApproved = false;
            result.RejectionReason = "Amount must be greater than zero";
            result.ProcessingSteps.Add("Basic Validation - FAILED");
            return result;
        }

        if (string.IsNullOrWhiteSpace(request.Currency))
        {
            result.IsApproved = false;
            result.RejectionReason = "Currency is required";
            result.ProcessingSteps.Add("Basic Validation - FAILED");
            return result;
        }

        result.ProcessingSteps.Add("Basic Validation - PASSED");

        // Step 2: Fraud Check (conditional)
        if (request.RequiresFraudCheck)
        {
            if (request.Amount > 10000)
            {
                result.IsApproved = false;
                result.RejectionReason = "Fraud check failed: Amount exceeds threshold";
                result.ProcessingSteps.Add("Fraud Check - FAILED");
                return result;
            }

            if (request.Country is "CU" or "IR" or "KP" or "SY")
            {
                result.IsApproved = false;
                result.RejectionReason = "Fraud check failed: Blocked country";
                result.ProcessingSteps.Add("Fraud Check - FAILED");
                return result;
            }

            result.ProcessingSteps.Add("Fraud Check - PASSED");
        }

        // Step 3: Compliance Check (conditional)
        if (request.RequiresComplianceCheck)
        {
            if (string.IsNullOrWhiteSpace(request.MerchantId))
            {
                result.IsApproved = false;
                result.RejectionReason = "Compliance check failed: Merchant ID required";
                result.ProcessingSteps.Add("Compliance Check - FAILED");
                return result;
            }

            if (request.Amount > 50000 && request.Country != "US")
            {
                result.IsApproved = false;
                result.RejectionReason = "Compliance check failed: Large international transaction requires additional verification";
                result.ProcessingSteps.Add("Compliance Check - FAILED");
                return result;
            }

            result.ProcessingSteps.Add("Compliance Check - PASSED");
        }

        // Step 4: Fee Calculation
        var fee = CalculateFee(request);
        result.CalculatedFee = fee;
        result.ProcessingSteps.Add($"Fee Calculation - ${fee:F2}");

        // Step 5: Final Approval
        result.IsApproved = true;
        result.ProcessingSteps.Add("Final Approval - APPROVED");

        return result;
    }

    private static decimal CalculateFee(PaymentProcessingRequest request)
    {
        // Simple fee calculation: 2.5% + $0.30
        var percentageFee = request.Amount * 0.025m;
        const decimal fixedFee = 0.30m;
        return Math.Round(percentageFee + fixedFee, 2);
    }
}

