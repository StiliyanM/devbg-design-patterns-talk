using DesignPatternsDemo.Strategy.After.Strategies.Interfaces;
using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public class AdyenPaymentStrategy : IAdyenPaymentStrategy
{
    public PaymentProvider Provider => PaymentProvider.Adyen;

    public decimal CalculateFee(PaymentRequest request)
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
}

