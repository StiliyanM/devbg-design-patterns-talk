using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.Common;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public class LocalBankPaymentStrategy : IPaymentProviderStrategy
{
    public PaymentProvider Provider => PaymentProvider.LocalBank;

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

        if (request.Country != "US")
        {
            return ValidationResult.Failure("LocalBank only supports US transactions");
        }

        if (request.IsRefund)
        {
            return ValidationResult.Failure("LocalBank does not support refunds");
        }

        return ValidationResult.Success();
    }

    public decimal CalculateFee(PaymentRequest request)
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
}

