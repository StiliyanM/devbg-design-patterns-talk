# Design Patterns I Actually Use: Practical Approaches for Real-World .NET Systems

Code from my talk for the DEV.BG .NET user group on 13 January 2026. I presented in Bulgarian to 60+ developers.

The examples use payment processing to show where a pattern earns its place: choosing provider behaviour, composing processing steps, and keeping business rules readable. Each pattern has a before-and-after implementation so you can compare the cost of the change with what it buys you.

[Event and agenda](https://dev.bg/event/design-patterns-i-actually-use-practical-approaches-for-real-world-net-systems/) · [My LinkedIn recap](https://www.linkedin.com/feed/update/urn:li:activity:7419727600765476864/)

## Code examples

- [Strategy](DesignPatternsDemo/Strategy/README.md): select payment-provider behaviour without spreading provider checks through the calling code.
- [Chain of Responsibility](DesignPatternsDemo/ChainOfResponsibility/README.md): split payment processing into steps you can compose and test separately.
- [Specification](DesignPatternsDemo/Specification/README.md): give business rules names and combine them without copying conditions.

The talk also covered CQRS and where MediatR fits. The code in this repository focuses on the 3 patterns above.
