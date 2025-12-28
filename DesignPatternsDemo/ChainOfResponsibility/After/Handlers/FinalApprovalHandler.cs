using DesignPatternsDemo.ChainOfResponsibility.Models;

namespace DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

public class FinalApprovalHandler : BasePaymentHandler, IFinalApprovalHandler
{
    public override bool CanHandle(PaymentProcessingRequest request) => true; // Always runs

    protected override void Process(PaymentProcessingRequest request, PaymentProcessingResult result)
    {
        result.ProcessingSteps.Add("Final Approval - STARTED");
        result.IsApproved = true;
        result.ProcessingSteps.Add("Final Approval - APPROVED");
    }
}

