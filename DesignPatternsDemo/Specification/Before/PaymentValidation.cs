using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.Common;

namespace DesignPatternsDemo.Specification.Before;

public class PaymentValidation
{
    public ValidationResult Validate(PaymentRequest request)
    {
        if (request.Provider == PaymentProvider.Stripe)
        {
            // Amount > 0
            if (request.Amount <= 0)
            {
                return ValidationResult.Failure("Amount must be greater than zero");
            }

            // Currency is required
            if (string.IsNullOrWhiteSpace(request.Currency))
            {
                return ValidationResult.Failure("Currency is required");
            }

            // Blocks transactions from: CU, IR, KP, SY
            var blockedCountries = new[] { "CU", "IR", "KP", "SY" };
            if (blockedCountries.Contains(request.Country.ToUpper()))
            {
                return ValidationResult.Failure($"Country {request.Country} is blocked");
            }

            // Refunds over 10,000 require manual approval
            if (request.IsRefund && request.Amount > 10000)
            {
                return ValidationResult.Failure("Refunds over $10,000 require manual approval");
            }
        }
        else if (request.Provider == PaymentProvider.Adyen)
        {
            // Amount > 0
            if (request.Amount <= 0)
            {
                return ValidationResult.Failure("Amount must be greater than zero");
            }

            // Currency is required
            if (string.IsNullOrWhiteSpace(request.Currency))
            {
                return ValidationResult.Failure("Currency is required");
            }

            // Only supports USD, EUR, GBP
            if (request.Currency is not ("USD" or "EUR" or "GBP"))
            {
                return ValidationResult.Failure("Adyen only supports USD, EUR, and GBP");
            }

            // Refunds over 5,000 require manual approval
            if (request.IsRefund && request.Amount > 5000)
            {
                return ValidationResult.Failure("Refunds over $5,000 require manual approval");
            }
        }
        else if (request.Provider == PaymentProvider.LocalBank)
        {
            // Amount > 0
            if (request.Amount <= 0)
            {
                return ValidationResult.Failure("Amount must be greater than zero");
            }

            // Currency is required
            if (string.IsNullOrWhiteSpace(request.Currency))
            {
                return ValidationResult.Failure("Currency is required");
            }

            // Only supports US transactions
            if (request.Country != "US")
            {
                return ValidationResult.Failure("LocalBank only supports US transactions");
            }

            // Refunds are not supported
            if (request.IsRefund)
            {
                return ValidationResult.Failure("LocalBank does not support refunds");
            }
        }

        return ValidationResult.Success();
    }
}

