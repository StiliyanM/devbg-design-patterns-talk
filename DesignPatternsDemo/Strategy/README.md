# Strategy Pattern - Payment Fee Calculation

## Business Requirements

This component calculates fees for payment requests using different payment providers. Each provider has unique fee calculation algorithms.

**Note:** This example focuses on fee calculation only. Validation is handled separately using the Specification pattern (see Specification/README.md).

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
- Does not support refunds

**Fee Calculation:**
- $2.00 flat fee + 1.5% of amount
- Fees rounded up to nearest cent
- Does not support refunds (throws exception)

## Implementation Approaches

### Before: Branching Logic

The `Before` implementation uses if/else statements to handle provider-specific logic. This approach:
- Requires modifying existing code when adding new providers
- Scatters provider logic across multiple methods
- Makes it difficult to test provider behavior in isolation

### After: Strategy Pattern

The `After` implementation uses the Strategy pattern to encapsulate provider-specific behavior. Each provider has its own strategy class implementing a common interface. This approach:
- Allows adding new providers without modifying existing code
- Isolates provider logic for easy testing
- Keeps orchestration logic simple and stable

