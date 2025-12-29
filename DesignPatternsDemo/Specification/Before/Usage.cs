using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.Before;

public static class Usage
{
    public static void Demonstrate()
    {
        Console.WriteLine("=== Payment Validation - BEFORE (Inline Rules) ===\n");

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

        Console.WriteLine("\n=== Issues with this approach ===");
        Console.WriteLine("• Rules are duplicated across providers (Amount > 0, Currency required)");
        Console.WriteLine("• Rules are tangled with provider logic");
        Console.WriteLine("• Hard to reuse rules in different contexts");
        Console.WriteLine("• Difficult to test individual rules in isolation");
        Console.WriteLine("• Adding new rules requires modifying multiple places");
    }
}

