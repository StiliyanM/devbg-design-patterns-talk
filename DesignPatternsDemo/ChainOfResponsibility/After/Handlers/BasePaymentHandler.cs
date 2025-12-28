using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

public abstract class BasePaymentHandler : IPaymentHandler
{
    private IPaymentHandler _nextHandler;

    public IPaymentHandler SetNext(IPaymentHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public abstract bool CanHandle(PaymentProcessingRequest request);

    public void Handle(PaymentProcessingRequest request, PaymentProcessingResult result)
    {
        if (!result.IsApproved)
        {
            return; // Stop if already rejected
        }

        if (!CanHandle(request))
        {
            // Skip this handler, continue to next
            _nextHandler?.Handle(request, result);
            return;
        }

        Process(request, result);

        if (result.IsApproved && _nextHandler != null)
        {
            _nextHandler.Handle(request, result);
        }
    }

    protected abstract void Process(PaymentProcessingRequest request, PaymentProcessingResult result);
}

