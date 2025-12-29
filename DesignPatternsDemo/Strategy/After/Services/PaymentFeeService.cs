using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Services;

public class PaymentFeeService(PaymentProviderStrategyFactory strategyFactory)
{
    public decimal CalculateFee(PaymentRequest request)
    {
        var strategy = strategyFactory.Create(request.Provider);
        return strategy.CalculateFee(request);
    }
}