using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After.Specifications;

public class CountryRestrictionSpec(string allowedCountry) : ISpecification<PaymentRequest>
{
    public bool IsSatisfiedBy(PaymentRequest candidate)
    {
        return candidate.Country == allowedCountry;
    }

    public string GetFailureMessage(PaymentRequest candidate)
    {
        return $"Only supports {allowedCountry} transactions";
    }
}

