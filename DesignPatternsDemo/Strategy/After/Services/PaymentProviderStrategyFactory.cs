using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.After.Strategies;

namespace DesignPatternsDemo.Strategy.After.Services;

public class PaymentProviderStrategyFactory
{
    private readonly Dictionary<PaymentProvider, IPaymentProviderStrategy> _strategies = new()
    {
        { PaymentProvider.Stripe, new StripePaymentStrategy() },
        { PaymentProvider.Adyen, new AdyenPaymentStrategy() },
        { PaymentProvider.LocalBank, new LocalBankPaymentStrategy() }
    };

    public IPaymentProviderStrategy Create(PaymentProvider provider) => 
        !_strategies.TryGetValue(provider, out var strategy) 
            ? throw new NotSupportedException($"Provider {provider} is not supported") 
            : strategy;
}

