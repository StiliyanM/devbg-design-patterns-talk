namespace DesignPatternsDemo.Specification.After.Specifications;

public static class SpecificationExtensions
{
    public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        return new AndSpecification<T>(left, right);
    }

    public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        return new OrSpecification<T>(left, right);
    }

    public static ISpecification<T> Not<T>(this ISpecification<T> spec)
    {
        return new NotSpecification<T>(spec);
    }
}

internal class AndSpecification<T>(ISpecification<T> left, ISpecification<T> right) : ISpecification<T>
{
    public bool IsSatisfiedBy(T candidate)
    {
        return left.IsSatisfiedBy(candidate) && right.IsSatisfiedBy(candidate);
    }

    public string GetFailureMessage(T candidate)
    {
        if (!left.IsSatisfiedBy(candidate))
        {
            return left.GetFailureMessage(candidate);
        }
        return right.GetFailureMessage(candidate);
    }
}

internal class OrSpecification<T>(ISpecification<T> left, ISpecification<T> right) : ISpecification<T>
{
    public bool IsSatisfiedBy(T candidate)
    {
        return left.IsSatisfiedBy(candidate) || right.IsSatisfiedBy(candidate);
    }

    public string GetFailureMessage(T candidate)
    {
        if (left.IsSatisfiedBy(candidate))
        {
            return string.Empty;
        }
        if (right.IsSatisfiedBy(candidate))
        {
            return string.Empty;
        }
        return $"Neither condition satisfied: {left.GetFailureMessage(candidate)} or {right.GetFailureMessage(candidate)}";
    }
}

internal class NotSpecification<T>(ISpecification<T> spec) : ISpecification<T>
{
    public bool IsSatisfiedBy(T candidate)
    {
        return !spec.IsSatisfiedBy(candidate);
    }

    public string GetFailureMessage(T candidate)
    {
        return spec.IsSatisfiedBy(candidate) ? "Not condition failed" : string.Empty;
    }
}

