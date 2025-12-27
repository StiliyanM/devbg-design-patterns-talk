using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.Common;

namespace DesignPatternsDemo.Strategy.After.Services;

public class PaymentFeeService(PaymentProviderStrategyFactory strategyFactory)
{
    public ValidationResult Validate(PaymentRequest request)
    {
        var strategy = strategyFactory.Create(request.Provider);
        return strategy.Validate(request);
    }

    public decimal CalculateFee(PaymentRequest request)
    {
        var strategy = strategyFactory.Create(request.Provider);
        return strategy.CalculateFee(request);
    }
}