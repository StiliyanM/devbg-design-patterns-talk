using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After.Specifications;

public class RefundSupportedSpec : ISpecification<PaymentRequest>
{
    public bool IsSatisfiedBy(PaymentRequest candidate)
    {
        return !candidate.IsRefund;
    }

    public string GetFailureMessage(PaymentRequest candidate)
    {
        return "Refunds are not supported";
    }
}

