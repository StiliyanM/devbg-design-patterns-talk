namespace DesignPatternsDemo.Strategy.After.Services;

public class ValidationResult
{
	public bool IsValid { get; private set; }
	public string? ErrorMessage { get; private set; }

	private ValidationResult(bool isValid, string? errorMessage = null)
	{
		IsValid = isValid;
		ErrorMessage = errorMessage;
	}

	public static ValidationResult Success() => new(true);
	public static ValidationResult Failure(string message) => new(false, message);
}