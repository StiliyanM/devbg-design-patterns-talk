using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After.Specifications;

public class CurrencyRequiredSpec : ISpecification<PaymentRequest>
{
    public bool IsSatisfiedBy(PaymentRequest candidate)
    {
        return !string.IsNullOrWhiteSpace(candidate.Currency);
    }

    public string GetFailureMessage(PaymentRequest candidate)
    {
        return "Currency is required";
    }
}

