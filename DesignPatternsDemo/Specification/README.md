# Specification Pattern - Payment Validation

## Business Requirements

This component validates payment requests using composable business rules. The focus is on validation rules (checks), not fee calculation.

**Note:** This example focuses on validation only. Fee calculation is handled separately using the Strategy pattern (see Strategy/README.md). In a complete system, you would use Specification for validation and Strategy for fee calculation algorithms.

### Payment Request

A `PaymentRequest` contains:
- **Provider**: The payment provider to use (Stripe, Adyen, or LocalBank)
- **Amount**: The payment amount
- **Currency**: The currency code (e.g., USD, EUR, GBP)
- **Country**: The country code (e.g., US, DE, FR)
- **IsRefund**: Whether this is a refund transaction

### Validation Rules by Provider

#### Stripe

- Amount must be greater than zero
- Currency is required
- Blocks transactions from: CU, IR, KP, SY
- Refunds over $10,000 require manual approval

#### Adyen

- Amount must be greater than zero
- Currency is required
- Only supports USD, EUR, and GBP
- Refunds over $5,000 require manual approval

#### LocalBank

- Amount must be greater than zero
- Currency is required
- Only supports US transactions (Country must be "US")
- Refunds are not supported

## Implementation Approaches

### Before: Inline Validation Logic

The `Before` implementation uses inline if statements.

**Issues:**
- Rules are duplicated across providers (Amount > 0, Currency required)
- Rules are tangled with provider logic
- Hard to reuse rules in different contexts
- Difficult to test individual rules in isolation
- Adding new rules requires modifying multiple places
- No way to compose or combine rules

### After: Specification Pattern

The `After` implementation uses the Specification pattern.

**Benefits:**
- Rules are reusable across providers
- Rules can be composed (And, Or, Not)
- Each rule is testable in isolation
- Provider validation is a clear composition of rules
- Easy to add new rules without modifying existing code
- Common rules (Amount > 0, Currency required) are defined once

## Specifications

Each business rule is represented as a specification:

- **AmountPositiveSpec**: Validates amount > 0
- **CurrencyRequiredSpec**: Validates currency is provided
- **SupportedCurrenciesSpec**: Validates currency is in allowed list
- **CountryNotBlockedSpec**: Validates country is not blocked
- **CountryRestrictionSpec**: Validates country matches requirement
- **RefundSupportedSpec**: Validates refunds are allowed
- **RefundLimitRequiresApprovalSpec**: Validates refund amount limits

## Composition

Specifications can be composed using:
- **And**: Both specifications must be satisfied
- **Or**: Either specification must be satisfied
- **Not**: Specification must not be satisfied

Example:
```csharp
var stripeSpec = amountPositive
    .And(currencyRequired)
    .And(new CountryNotBlockedSpec("CU", "IR", "KP", "SY"))
    .And(new RefundLimitRequiresApprovalSpec(10000));
```

## When NOT to Use Specification Pattern

The pattern adds complexity and should be avoided when:

1. **Simple one-off checks**
   - A single validation rule used only once
   - The overhead of creating a specification class isn't justified

2. **No reuse or composition needed**
   - Rules are never reused or combined
   - Each validation is unique and standalone

3. **Rules are always used together**
   - If rules always appear as a fixed set
   - Composition adds no value over a simple method

4. **Overengineering for trivial logic**
   - Simple validation that doesn't benefit from abstraction
   - The pattern's flexibility isn't needed

## How Specification and Strategy Work Together

In a realistic payment system:

1. **Specification Pattern** handles validation:
   - Composable, reusable rules
   - Easy to test and modify
   - Rules can be shared across providers

2. **Strategy Pattern** handles algorithms:
   - Provider-specific fee calculation
   - Provider-specific processing logic
   - Different behaviors per provider

They complement each other:
- **Specification** = "What rules must pass?" (validation)
- **Strategy** = "How do I process it?" (algorithm)

Example workflow:
```csharp
// Step 1: Validate using Specification
var validation = new PaymentValidation();
var result = validation.Validate(request);
if (!result.IsValid) return result;

// Step 2: Calculate fee using Strategy
var feeService = new PaymentFeeService(factory);
var fee = feeService.CalculateFee(request);
```

## Example: When Specification is Overkill

```csharp
// Simple validation - Specification would be overkill
public bool IsValidEmail(string email)
{
    return email.Contains("@") && email.Contains(".");
}
```

In this case, a simple method is clearer than creating a specification class.

