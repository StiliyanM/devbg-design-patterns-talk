using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.Before.Services;

public class PaymentFeeService
{
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

