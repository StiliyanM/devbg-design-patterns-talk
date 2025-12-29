using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After.Specifications;

public class AmountPositiveSpec : ISpecification<PaymentRequest>
{
    public bool IsSatisfiedBy(PaymentRequest candidate)
    {
        return candidate.Amount > 0;
    }

    public string GetFailureMessage(PaymentRequest candidate)
    {
        return "Amount must be greater than zero";
    }
}

