# Strategy Pattern - Payment Fee Calculation

## Business Requirements

This component calculates fees for payment requests using different payment providers. Each provider has unique fee calculation algorithms.

### Payment Request

A `PaymentRequest` contains:
- **Provider**: The payment provider to use (Stripe, Adyen, or LocalBank)
- **Amount**: The payment amount
- **Currency**: The currency code (e.g., USD, EUR, GBP)
- **Country**: The country code (e.g., US, DE, FR)
- **IsRefund**: Whether this is a refund transaction

### Payment Providers

#### Stripe

**Fee Calculation:**
- 2.9% of amount + $0.30 fixed fee
- No fees charged on refunds
- Fees rounded to 2 decimal places using standard rounding

#### Adyen

**Fee Calculation:**
- Percentage fee varies by currency:
  - USD: 2.5%
  - EUR: 2.4%
  - GBP: 2.6%
- $0.25 fixed fee
- Minimum fee of $0.50
- No fees charged on refunds
- Fees rounded to 2 decimal places

#### LocalBank

**Fee Calculation:**
- $2.00 flat fee + 1.5% of amount
- Fees rounded up to nearest cent
- Does not support refunds (throws exception)

## Implementation Approaches

### Before: Branching Logic

The `Before` implementation uses if/else statements to handle provider-specific logic. This approach:

**Issues:**
- Adding a new provider requires modifying `CalculateFee()` method
- Provider-specific logic is hardcoded with if/else statements
- Risk of breaking existing providers when adding new ones
- Difficult to test provider logic in isolation
- Provider logic is scattered across multiple methods

### After: Strategy Pattern

The `After` implementation uses the Strategy pattern to encapsulate provider-specific behavior. Each provider has its own strategy class implementing a common interface. This approach:

**Benefits:**
- New providers can be added by creating a new strategy class
- No need to modify existing code when adding providers
- Provider logic is isolated and easily testable
- Service orchestration logic is simple and stable
- Each provider's behavior is self-contained

