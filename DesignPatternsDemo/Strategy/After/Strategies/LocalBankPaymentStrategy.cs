using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public class LocalBankPaymentStrategy : IPaymentProviderStrategy
{
    public PaymentProvider Provider => PaymentProvider.LocalBank;

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

