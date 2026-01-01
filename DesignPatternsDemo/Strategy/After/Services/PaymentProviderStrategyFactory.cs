using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.After.Strategies;
using DesignPatternsDemo.Strategy.After.Strategies.Interfaces;

namespace DesignPatternsDemo.Strategy.After.Services;

public class PaymentProviderStrategyFactory(
    IStripePaymentStrategy stripe,
    IAdyenPaymentStrategy adyen,
    ILocalBankPaymentStrategy localBank)
{
    private readonly Dictionary<PaymentProvider, IPaymentProviderStrategy> _strategies = new()
    {
        { PaymentProvider.Stripe, stripe },
        { PaymentProvider.Adyen, adyen },
        { PaymentProvider.LocalBank, localBank }
    };

    public IPaymentProviderStrategy Create(PaymentProvider provider) => 
        !_strategies.TryGetValue(provider, out var strategy) 
            ? throw new NotSupportedException($"Provider {provider} is not supported") 
            : strategy;
}

