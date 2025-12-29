namespace DesignPatternsDemo.Specification.After.Specifications;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T candidate);
    string GetFailureMessage(T candidate);
}

