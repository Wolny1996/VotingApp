using System.Net;

namespace VotingApp.Domain.Abstraction;

/// <summary>
/// Custom model that represtens an expected failure (error).
/// Its aim to replace the approach of throwing out exceptions in expected situation (when we know how to deal with them).
/// It is strongly linked with custom <see cref="Result"/> model approach.
/// </summary>
/// <param name="Type">Indicates the type of error.
/// It should begin with property or type name to which it refers and then describe the error using PascalCase.
/// Example: Dates.StartDateGraterThenEndDate.
/// </param>
/// <param name="Message">Message describing the problem.</param>
/// It should describe the error.
/// Example: The specified result value is null.
/// </param>
public sealed record Error(
    string Type,
    string Message,
    HttpStatusCode HttpStatusCode = HttpStatusCode.BadRequest)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error NullValue = new("Errors.NullValue", "The specified result value is null.");

    /// <summary>
    /// Code by which error will be implicitly converted to a custom <see cref="Result"/> type,
    /// which simplifies the way they are declared.
    /// </summary>
    /// <param name="error">Error occurrence</param>
    public static implicit operator Result(Error error) => Result.Failure(error);
}
