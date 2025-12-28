using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.Before;

public static class Usage
{
    public static void Demonstrate()
    {
        Console.WriteLine("=== Payment Processing - BEFORE (Rigid Flow) ===\n");

        var processor = new PaymentProcessor();

        var testCases = new[]
        {
            new PaymentProcessingRequest
            {
                PaymentId = "PAY-001",
                Amount = 1000.00m,
                Currency = "USD",
                Country = "US",
                MerchantId = "MERCH-001",
                RequiresFraudCheck = true,
                RequiresComplianceCheck = true
            },
            new PaymentProcessingRequest
            {
                PaymentId = "PAY-002",
                Amount = 15000.00m,
                Currency = "USD",
                Country = "US",
                MerchantId = "MERCH-001",
                RequiresFraudCheck = true,
                RequiresComplianceCheck = false
            },
            new PaymentProcessingRequest
            {
                PaymentId = "",
                Amount = 500.00m,
                Currency = "USD",
                Country = "US",
                RequiresFraudCheck = false,
                RequiresComplianceCheck = false
            }
        };

        foreach (var request in testCases)
        {
            Console.WriteLine($"Processing Payment: {request.PaymentId}");
            Console.WriteLine($"  Amount: {request.Amount} {request.Currency}");
            Console.WriteLine($"  Country: {request.Country}");
            Console.WriteLine($"  Fraud Check: {request.RequiresFraudCheck}");
            Console.WriteLine($"  Compliance Check: {request.RequiresComplianceCheck}");

            var result = processor.ProcessPayment(request);

            Console.WriteLine($"  Result: {(result.IsApproved ? "✅ APPROVED" : "❌ REJECTED")}");
            if (!result.IsApproved)
            {
                Console.WriteLine($"  Reason: {result.RejectionReason}");
            }
            else
            {
                Console.WriteLine($"  Fee: ${result.CalculatedFee:F2}");
            }

            Console.WriteLine("  Steps:");
            foreach (var step in result.ProcessingSteps)
            {
                Console.WriteLine($"    - {step}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("\n=== Issues with this approach ===");
        Console.WriteLine("• Flow is rigid - steps are hardcoded in fixed order");
        Console.WriteLine("• Adding a new step requires modifying ProcessPayment()");
        Console.WriteLine("• Reordering steps is risky and error-prone");
        Console.WriteLine("• Method grows and becomes harder to reason about");
        Console.WriteLine("• Conditional steps (fraud/compliance) add complexity");
        Console.WriteLine("• Difficult to test individual steps in isolation");
    }
}

