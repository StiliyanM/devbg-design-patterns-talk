using DesignPatternsDemo.Strategy.After.Strategies.Interfaces;
using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public class StripePaymentStrategy : BasePaymentStrategy, IStripePaymentStrategy
{
    public override PaymentProvider Provider => PaymentProvider.Stripe;

    protected override decimal CalculateRegularFee(PaymentRequest request)
    {
        var baseFee = request.Amount * 0.029m; // 2.9%
        var fixedFee = 0.30m;
        var totalFee = baseFee + fixedFee;

        // Stripe rounds to 2 decimal places
        return Math.Round(totalFee, 2, MidpointRounding.AwayFromZero);
    }
}

