using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public class StripePaymentStrategy : IPaymentProviderStrategy
{
    public PaymentProvider Provider => PaymentProvider.Stripe;

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

