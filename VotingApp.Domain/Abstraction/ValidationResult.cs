namespace VotingApp.Domain.Abstraction;

/// <summary>
/// ValidationResult model is a continuation of the <see cref="Result"/> model approach and it is way to express result of validation action.
/// Model based on the <see cref="IValidationResult"/> interface which is used by the FluentValdiation library.
/// It describes all valdiation errors for a given Command/Query object.
/// It is strongly linked with custom <see cref="Result"/> and <see cref="Error"/> models approach.
/// </summary>
public sealed class ValidationResult : Result, IValidationResult
{
    public ValidationResult(Error[] validationErrors)
        : base(false, IValidationResult.ValdiationError) => ValidationErrors = validationErrors;

    public Error[] ValidationErrors { get; init; }

    public static ValidationResult WithErrors(Error[] validationErrors) => new(validationErrors);
}

/// <summary>
/// Generic version of ValidationResult.
/// </summary>
/// <typeparam name="TValue">The generic type of <see cref="Result"/> model.</typeparam>
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public ValidationResult(Error[] validationErrors)
        : base(default, false, IValidationResult.ValdiationError) => ValidationErrors = validationErrors;

    public Error[] ValidationErrors { get; init; }

    public static ValidationResult<TValue> WithErrors(Error[] validationErrors) => new(validationErrors);
}