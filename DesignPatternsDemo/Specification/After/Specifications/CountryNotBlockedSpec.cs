using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After.Specifications;

public class CountryNotBlockedSpec(params string[] blockedCountries) : ISpecification<PaymentRequest>
{
    public bool IsSatisfiedBy(PaymentRequest candidate)
    {
        return !blockedCountries.Contains(candidate.Country.ToUpper());
    }

    public string GetFailureMessage(PaymentRequest candidate)
    {
        return $"Country {candidate.Country} is blocked";
    }
}

