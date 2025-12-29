using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After.Specifications;

public class SupportedCurrenciesSpec(params string[] supportedCurrencies) : ISpecification<PaymentRequest>
{
    public bool IsSatisfiedBy(PaymentRequest candidate)
    {
        return supportedCurrencies.Contains(candidate.Currency);
    }

    public string GetFailureMessage(PaymentRequest candidate)
    {
        var currencies = string.Join(", ", supportedCurrencies);
        return $"Only supports {currencies}";
    }
}

