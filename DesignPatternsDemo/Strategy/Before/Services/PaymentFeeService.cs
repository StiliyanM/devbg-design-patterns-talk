using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.Common;

namespace DesignPatternsDemo.Strategy.Before.Services;

public class PaymentFeeService
{
    public ValidationResult Validate(PaymentRequest request)
    {
        if (request.Provider == PaymentProvider.Stripe)
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
        }
        else if (request.Provider == PaymentProvider.Adyen)
        {
            if (request.Amount <= 0)
            {
                return ValidationResult.Failure("Amount must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(request.Currency))
            {
                return ValidationResult.Failure("Currency is required");
            }

            if (request.Currency is not ("USD" or "EUR" or "GBP"))
            {
                return ValidationResult.Failure("Adyen only supports USD, EUR, and GBP");
            }

            if (request is { IsRefund: true, Amount: > 5000 })
            {
                return ValidationResult.Failure("Refunds over $5,000 require manual approval");
            }
        }
        else if (request.Provider == PaymentProvider.LocalBank)
        {
            if (request.Amount <= 0)
            {
                return ValidationResult.Failure("Amount must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(request.Currency))
            {
                return ValidationResult.Failure("Currency is required");
            }

            if (request.Country != "US")
            {
                return ValidationResult.Failure("LocalBank only supports US transactions");
            }

            if (request.IsRefund)
            {
                return ValidationResult.Failure("LocalBank does not support refunds");
            }
        }

        return ValidationResult.Success();
    }

    public decimal CalculateFee(PaymentRequest request)
    {
        if (request.Provider == PaymentProvider.Stripe)
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

        if (request.Provider == PaymentProvider.Adyen)
        {
            if (request.IsRefund)
            {
                return 0; // Adyen doesn't charge fees on refunds
            }

            var feePercentage = request.Currency switch
            {
                "USD" => 0.025m, // 2.5%
                "EUR" => 0.024m, // 2.4%
                "GBP" => 0.026m, // 2.6%
                _ => 0.025m
            };

            var baseFee = request.Amount * feePercentage;
            var fixedFee = 0.25m;
            var totalFee = baseFee + fixedFee;

            // Adyen has a minimum fee of $0.50
            var minimumFee = 0.50m;
            if (totalFee < minimumFee)
            {
                totalFee = minimumFee;
            }

            return Math.Round(totalFee, 2, MidpointRounding.AwayFromZero);
        }

        if (request.Provider == PaymentProvider.LocalBank)
        {
            if (request.IsRefund)
            {
                throw new InvalidOperationException("LocalBank does not support refunds");
            }

            // LocalBank has a flat fee structure
            var flatFee = 2.00m;
            var percentageFee = request.Amount * 0.015m; // 1.5%
            var totalFee = flatFee + percentageFee;

            // LocalBank rounds up to nearest cent
            return Math.Ceiling(totalFee * 100) / 100;
        }

        throw new NotSupportedException($"Provider {request.Provider} is not supported");
    }
}

