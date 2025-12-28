using DesignPatternsDemo.ChainOfResponsibility.After.Handlers;

namespace DesignPatternsDemo.ChainOfResponsibility.After;

public interface IPaymentHandlerChainFactory
{
    IPaymentHandler CreateChain();
}

public class PaymentHandlerChainFactory(
    IValidationHandler validation,
    IFraudCheckHandler fraudCheck,
    IComplianceHandler compliance,
    IFeeCalculationHandler feeCalculation,
    IFinalApprovalHandler finalApproval)
    : IPaymentHandlerChainFactory
{
    public IPaymentHandler CreateChain() =>
        validation
            .SetNext(fraudCheck)
            .SetNext(compliance)
            .SetNext(feeCalculation)
            .SetNext(finalApproval);
}

