using DesignPatternsDemo.Strategy.After.Strategies.Interfaces;
using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Strategy.After.Strategies;

public abstract class BasePaymentStrategy : IPaymentProviderStrategy
{
    public abstract PaymentProvider Provider { get; }

    public decimal CalculateFee(PaymentRequest request) => 
        request.IsRefund ? GetRefundFee() : CalculateRegularFee(request);

    protected virtual decimal GetRefundFee()
    {
        return 0; // Default: no fee on refunds
    }

    protected abstract decimal CalculateRegularFee(PaymentRequest request);
}

