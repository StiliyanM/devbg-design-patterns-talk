# Chain of Responsibility Pattern - Payment Processing Flow

## Business Requirements

This component processes payment requests through a series of validation and processing steps. The flow must be flexible to accommodate different payment types and evolving business rules.

### Payment Processing Steps

A payment goes through several processing steps:

1. **Basic Validation**
   - Payment ID is required
   - Amount must be greater than zero
   - Currency is required

2. **Fraud Check** (conditional)
   - Only runs if `RequiresFraudCheck` is true
   - Rejects if amount exceeds $10,000
   - Rejects transactions from blocked countries (CU, IR, KP, SY)

3. **Compliance Check** (conditional)
   - Only runs if `RequiresComplianceCheck` is true
   - Requires Merchant ID
   - Large international transactions (>$50,000) require additional verification

4. **Fee Calculation**
   - Calculates processing fee: 2.5% of amount + $0.30 fixed fee
   - Always runs if previous steps pass

5. **Final Approval**
   - Approves the payment if all previous steps passed

### Key Characteristics

- Not all steps apply to all payments
- The order of steps may need to change over time
- Steps can be added or removed as business requirements evolve
- Each step can reject the payment, stopping further processing

## Implementation Approaches

### Before: Rigid Flow

The `Before` implementation uses a single method with hardcoded steps.

**Issues:**
- Flow is rigid - steps are hardcoded in fixed order
- Adding a new step requires modifying `ProcessPayment()` method
- Reordering steps is risky and error-prone
- Method grows and becomes harder to reason about
- Conditional steps (fraud/compliance) add complexity
- Difficult to test individual steps in isolation

### After: Chain of Responsibility

The `After` implementation uses the Chain of Responsibility pattern:
- Each step is a separate handler
- Handlers are chained together
- Each handler decides whether to continue
- Processing is composed, not hardcoded

**Benefits:**
- Steps are easy to add or remove - just modify the chain
- Order can be changed safely - reorder handlers in chain
- Logic is easier to test - each handler can be tested in isolation
- Flow is easier to understand - each handler has one responsibility
- Conditional steps are handled by the handler itself

## When NOT to Use Chain of Responsibility

The pattern adds complexity and should be avoided when:

1. **Simple flows with 1-2 fixed steps**
   - The overhead of handlers outweighs the benefits
   - A simple method is clearer and more maintainable

2. **Fixed order that never changes**
   - If the order is truly fixed and unlikely to evolve
   - A rigid flow may be simpler and more direct

3. **Steps are tightly coupled**
   - If steps must always run together as a unit
   - The pattern's flexibility isn't needed

4. **Overhead outweighs benefits**
   - For simple, stable workflows
   - The pattern adds indirection that may not be justified

## Example: When CoR is Overkill

```csharp
// Simple two-step process - CoR would be overengineering
public Result ProcessSimplePayment(PaymentRequest request)
{
    if (!Validate(request)) return Result.Failure("Invalid");
    return CalculateFee(request);
}
```

In this case, a simple method is clearer than creating handlers and a chain.

