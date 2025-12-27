using DesignPatternsDemo.Strategy.After.Services;
using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After;

public static class Usage
{
    public static void Demonstrate()
    {
        Console.WriteLine("=== Payment Fee Calculation - AFTER (Strategy Pattern) ===\n");

        var strategyFactory = new PaymentProviderStrategyFactory();
        var service = new PaymentFeeService(strategyFactory);

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

            var validation = service.Validate(request);
            if (!validation.IsValid)
            {
                Console.WriteLine($"  ❌ Validation failed: {validation.ErrorMessage}");
            }
            else
            {
                try
                {
                    var fee = service.CalculateFee(request);
                    Console.WriteLine($"  ✅ Fee: {fee} {request.Currency}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  ❌ Error: {ex.Message}");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("\n=== Benefits of Strategy Pattern ===");
        Console.WriteLine("• New providers can be added by creating a new strategy class");
        Console.WriteLine("• No need to modify existing code when adding providers");
        Console.WriteLine("• Provider logic is isolated and easily testable");
        Console.WriteLine("• Service orchestration logic is simple and stable");
        Console.WriteLine("• Each provider's behavior is self-contained");
    }
}

