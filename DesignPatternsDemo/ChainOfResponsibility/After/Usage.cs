using DesignPatternsDemo.ChainOfResponsibility.Models;
using DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

namespace DesignPatternsDemo.ChainOfResponsibility.After;

public static class Usage
{
    public static void Demonstrate()
    {
        Console.WriteLine("=== Payment Processing - AFTER (Chain of Responsibility) ===\n");

        // Create handlers (in real app, these would be injected via DI)
        IValidationHandler validation = new ValidationHandler();
        IFraudCheckHandler fraudCheck = new FraudCheckHandler();
        IComplianceHandler compliance = new ComplianceHandler();
        IFeeCalculationHandler feeCalculation = new FeeCalculationHandler();
        IFinalApprovalHandler finalApproval = new FinalApprovalHandler();

        // Create factory with dependency injection
        var factory = new PaymentHandlerChainFactory(
            validation,
            fraudCheck,
            compliance,
            feeCalculation,
            finalApproval);

        var processor = new PaymentProcessor(factory);

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

        Console.WriteLine("\n=== Benefits of Chain of Responsibility ===");
        Console.WriteLine("• Steps are easy to add or remove - just modify the chain");
        Console.WriteLine("• Order can be changed safely - reorder handlers in chain");
        Console.WriteLine("• Logic is easier to test - each handler can be tested in isolation");
        Console.WriteLine("• Flow is easier to understand - each handler has one responsibility");
        Console.WriteLine("• Conditional steps are handled by the handler itself");
        Console.WriteLine("\n=== When NOT to use Chain of Responsibility ===");
        Console.WriteLine("• Simple flows with 1-2 fixed steps - adds unnecessary indirection");
        Console.WriteLine("• Fixed order that never changes - rigid flow may be simpler");
        Console.WriteLine("• Steps are tightly coupled - if steps must always run together");
        Console.WriteLine("• Overhead outweighs benefits - pattern adds complexity");
    }
}

