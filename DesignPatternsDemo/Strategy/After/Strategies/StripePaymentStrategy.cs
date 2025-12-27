using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.Common;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public class StripePaymentStrategy : IPaymentProviderStrategy
{
    public PaymentProvider Provider => PaymentProvider.Stripe;

    public ValidationResult Validate(PaymentRequest request)
    {
        if (request.Amount <= 0)
        {
            return ValidationResult.Failure("Amount must be greater than zero");
        }

        if (string.IsNullOrWhiteSpace(request.Currency))
        {
            return ValidationResult.Failure("Currency is required");
        }

        var blockedCountries = new[] { "CU", "IR", "KP", "SY" };
        if (blockedCountries.Contains(request.Country.ToUpper()))
        {
            return ValidationResult.Failure($"Country {request.Country} is blocked");
        }

        if (request is { IsRefund: true, Amount: > 10000 })
        {
            return ValidationResult.Failure("Refunds over $10,000 require manual approval");
        }

        return ValidationResult.Success();
    }

    public decimal CalculateFee(PaymentRequest request)
    {
        if (request.IsRefund)
        {
            return 0; // Stripe doesn't charge fees on refunds
        }

        var baseFee = request.Amount * 0.029m; // 2.9%
        var fixedFee = 0.30m;
        var totalFee = baseFee + fixedFee;

        // Stripe rounds to 2 decimal places
        return Math.Round(totalFee, 2, MidpointRounding.AwayFromZero);
    }
}

