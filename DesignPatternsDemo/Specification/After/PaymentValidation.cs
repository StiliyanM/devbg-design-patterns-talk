using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.Common;
using DesignPatternsDemo.Specification.After.Specifications;

namespace DesignPatternsDemo.Specification.After;

public class PaymentValidation
{
    private readonly ISpecification<PaymentRequest> _stripeSpec;
    private readonly ISpecification<PaymentRequest> _adyenSpec;
    private readonly ISpecification<PaymentRequest> _localBankSpec;

    public PaymentValidation()
    {
        // Common specs
        var amountPositive = new AmountPositiveSpec();
        var currencyRequired = new CurrencyRequiredSpec();

        // Stripe specification composition
        _stripeSpec = amountPositive
            .And(currencyRequired)
            .And(new CountryNotBlockedSpec("CU", "IR", "KP", "SY"))
            .And(new RefundLimitRequiresApprovalSpec(10000));

        // Adyen specification composition
        _adyenSpec = amountPositive
            .And(currencyRequired)
            .And(new SupportedCurrenciesSpec("USD", "EUR", "GBP"))
            .And(new RefundLimitRequiresApprovalSpec(5000));

        // LocalBank specification composition
        _localBankSpec = amountPositive
            .And(currencyRequired)
            .And(new CountryRestrictionSpec("US"))
            .And(new RefundSupportedSpec());
    }

    public ValidationResult Validate(PaymentRequest request)
    {
        var spec = request.Provider switch
        {
            PaymentProvider.Stripe => _stripeSpec,
            PaymentProvider.Adyen => _adyenSpec,
            PaymentProvider.LocalBank => _localBankSpec,
            _ => throw new NotSupportedException($"Provider {request.Provider} is not supported")
        };

        return spec.IsSatisfiedBy(request) ? 
            ValidationResult.Success() : 
            ValidationResult.Failure(spec.GetFailureMessage(request));
    }
}

