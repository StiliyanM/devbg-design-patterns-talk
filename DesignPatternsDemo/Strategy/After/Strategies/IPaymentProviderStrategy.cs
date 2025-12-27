using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.Common;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public interface IPaymentProviderStrategy
{
    PaymentProvider Provider { get; }
    ValidationResult Validate(PaymentRequest request);
    decimal CalculateFee(PaymentRequest request);
}

