using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After;

public class PaymentProcessor(IPaymentHandlerChainFactory chainFactory)
{
    public PaymentProcessingResult ProcessPayment(PaymentProcessingRequest request)
    {
        var result = new PaymentProcessingResult
        {
            IsApproved = true, // Start as approved, handlers will reject if needed
            ProcessingSteps = []
        };

        var chain = chainFactory.CreateChain();
        chain.Handle(request, result);

        return result;
    }
}

