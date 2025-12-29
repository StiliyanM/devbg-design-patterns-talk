using DesignPatternsDemo.Strategy.Models;

namespace DesignPatternsDemo.Specification.After.Specifications;

public class RefundLimitRequiresApprovalSpec(decimal limit) : ISpecification<PaymentRequest>
{
    public bool IsSatisfiedBy(PaymentRequest candidate)
    {
        if (!candidate.IsRefund)
        {
            return true; // Not a refund, so this rule doesn't apply
        }
        return candidate.Amount <= limit;
    }

    public string GetFailureMessage(PaymentRequest candidate)
    {
        return $"Refunds over ${limit:N0} require manual approval";
    }
}

