using DesignPatternsDemo.Strategy.Before.Services;
using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.Before;

public static class Usage
{
    public static void Demonstrate()
    {
        Console.WriteLine("=== Payment Fee Calculation - BEFORE (Branching Logic) ===\n");

        var service = new PaymentFeeService();

        var testCases = new[]
        {
            new PaymentRequest
            {
                Provider = PaymentProvider.Stripe,
                Amount = 100.00m,
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
                Provider = PaymentProvider.LocalBank,
                Amount = 500.00m,
                Currency = "USD",
                Country = "US",
                IsRefund = false
            },
            new PaymentRequest
            {
                Provider = PaymentProvider.Stripe,
                Amount = 50.00m,
                Currency = "USD",
                Country = "US",
                IsRefund = true
            }
        };

        foreach (var request in testCases)
        {
            Console.WriteLine($"Processing {request.Provider} payment:");
            Console.WriteLine($"  Amount: {request.Amount} {request.Currency}");
            Console.WriteLine($"  Country: {request.Country}");
            Console.WriteLine($"  IsRefund: {request.IsRefund}");

            try
            {
                var fee = service.CalculateFee(request);
                Console.WriteLine($"  ✅ Fee: {fee} {request.Currency}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Error: {ex.Message}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("\n=== Issues with this approach ===");
        Console.WriteLine("• Adding a new provider requires modifying CalculateFee()");
        Console.WriteLine("• Provider-specific logic is hardcoded with if/else statements");
        Console.WriteLine("• Risk of breaking existing providers when adding new ones");
        Console.WriteLine("• Difficult to test provider logic in isolation");
    }
}

