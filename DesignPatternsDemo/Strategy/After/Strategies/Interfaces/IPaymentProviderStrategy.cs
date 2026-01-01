using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Strategies.Interfaces;

public interface IPaymentProviderStrategy
{
    PaymentProvider Provider { get; }
    decimal CalculateFee(PaymentRequest request);
}

