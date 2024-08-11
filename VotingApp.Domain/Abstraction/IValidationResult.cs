namespace VotingApp.Domain.Abstraction;

public interface IValidationResult
{
    public static readonly Error ValdiationError = new("ValdiationErrors", "A valdiation issue has occured.");

    /// <summary>
    /// A property that stores all errors after valdiation of a given Command/Query object.
    /// </summary>
    Error[] ValidationErrors { get; init; }
}