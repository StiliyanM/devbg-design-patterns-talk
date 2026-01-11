using DesignPatternsDemo.Strategy.Models;
using DesignPatternsDemo.Strategy.After.Services;
using DesignPatternsDemo.Strategy.After.Strategies;
using DesignPatternsDemo.Specification.After;
using DesignPatternsDemo.ChainOfResponsibility.After;
using DesignPatternsDemo.ChainOfResponsibility.After.Handlers;
using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo;

public static class CombinedUsage
{
    public static void Demonstrate()
    {
        // Initialize patterns (in real app, injected via DI)
        var validation = new PaymentValidation();
        var feeService = new PaymentFeeService(new PaymentProviderStrategyFactory(
            new StripePaymentStrategy(),
            new AdyenPaymentStrategy(),
            new LocalBankPaymentStrategy()));
        var processor = new PaymentProcessor(new PaymentHandlerChainFactory(
            new ValidationHandler(),
            new FraudCheckHandler(),
            new ComplianceHandler(),
            new FeeCalculationHandler(),
            new FinalApprovalHandler()));

        var request = new PaymentRequest
        {
            Provider = PaymentProvider.Stripe,
            Amount = 1000.00m,
            Currency = "USD",
            Country = "US",
            IsRefund = false
        };


        // 1. Specification Pattern: Validation
        var validationResult = validation.Validate(request);
        if (!validationResult.IsValid)
        {
            return;
        }

        // 2. Strategy Pattern: Fee calculation
        var fee = feeService.CalculateFee(request);

        // 3. Chain of Responsibility: Processing workflow
        var processingRequest = new PaymentProcessingRequest
        {
            PaymentId = $"PAY-{Guid.NewGuid():N}",
            Amount = request.Amount,
            Currency = request.Currency,
            Country = request.Country,
            MerchantId = "MERCH-001",
            RequiresFraudCheck = request.Amount > 10000,
            RequiresComplianceCheck = false
        };

        var result = processor.ProcessPayment(processingRequest);
    }
}

