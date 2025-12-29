using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After;

public static class Usage
{
    public static void Demonstrate()
    {
        Console.WriteLine("=== Payment Validation - AFTER (Specification Pattern) ===\n");

        var validation = new PaymentValidation();

        var testCases = new[]
        {
            new PaymentRequest
            {
                Provider = PaymentProvider.Stripe,
                Amount = 1000.00m,
                Currency = "USD",
                Country = "US",
                IsRefund = false
            },
            new PaymentRequest
            {
                Provider = PaymentProvider.Adyen,
                Amount = 250.00m,
                Currency = "EUR",
                Country = "DE",
                IsRefund = false
            },
            new PaymentRequest
            {
                Provider = PaymentProvider.Stripe,
                Amount = 15000.00m,
                Currency = "USD",
                Country = "US",
                IsRefund = true
            },
            new PaymentRequest
            {
                Provider = PaymentProvider.LocalBank,
                Amount = 500.00m,
                Currency = "USD",
                Country = "CA",
                IsRefund = false
            },
            new PaymentRequest
            {
                Provider = PaymentProvider.Adyen,
                Amount = 0m,
                Currency = "USD",
                Country = "US",
                IsRefund = false
            }
        };

        foreach (var request in testCases)
        {
            Console.WriteLine($"Validating {request.Provider} payment:");
            Console.WriteLine($"  Amount: {request.Amount} {request.Currency}");
            Console.WriteLine($"  Country: {request.Country}");
            Console.WriteLine($"  IsRefund: {request.IsRefund}");

            var result = validation.Validate(request);

            if (result.IsValid)
            {
                Console.WriteLine("  ✅ Validation PASSED");
            }
            else
            {
                Console.WriteLine($"  ❌ Validation FAILED: {result.ErrorMessage}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("\n=== Benefits of Specification Pattern ===");
        Console.WriteLine("• Rules are reusable across providers");
        Console.WriteLine("• Rules can be composed (And, Or, Not)");
        Console.WriteLine("• Each rule is testable in isolation");
        Console.WriteLine("• Provider validation is a clear composition of rules");
        Console.WriteLine("• Easy to add new rules without modifying existing code");
        Console.WriteLine("\n=== When NOT to use Specification Pattern ===");
        Console.WriteLine("• Simple one-off checks - adds unnecessary abstraction");
        Console.WriteLine("• No reuse or composition needed - overhead not justified");
        Console.WriteLine("• Rules are always used together - composition adds no value");
        Console.WriteLine("• Overengineering for trivial validation logic");
    }
}

