using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

public interface IPaymentHandler
{
    IPaymentHandler SetNext(IPaymentHandler handler);
    bool CanHandle(PaymentProcessingRequest request);
    void Handle(PaymentProcessingRequest request, PaymentProcessingResult result);
}

