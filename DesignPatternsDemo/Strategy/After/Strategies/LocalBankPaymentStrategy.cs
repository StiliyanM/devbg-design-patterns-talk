using DesignPatternsDemo.Strategy.After.Strategies.Interfaces;
using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public class LocalBankPaymentStrategy : BasePaymentStrategy, ILocalBankPaymentStrategy
{
    public override PaymentProvider Provider => PaymentProvider.LocalBank;

    protected override decimal GetRefundFee()
    {
        throw new InvalidOperationException("LocalBank does not support refunds");
    }

    protected override decimal CalculateRegularFee(PaymentRequest request)
    {
        // LocalBank has a flat fee structure
        const decimal flatFee = 2.00m;
        var percentageFee = request.Amount * 0.015m; // 1.5%
        var totalFee = flatFee + percentageFee;

        // LocalBank rounds up to nearest cent
        return Math.Ceiling(totalFee * 100) / 100;
    }
}

