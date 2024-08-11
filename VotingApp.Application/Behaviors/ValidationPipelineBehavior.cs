using FluentValidation;
using MediatR;
using VotingApp.Domain.Abstraction;

namespace VotingApp.Application.Behaviors;

/// <summary>
/// Pipeline behavior which determining how each Command/Query is to be validated.
/// Called every time the Send( ) method od MEdiatR library is called.
/// </summary>
/// <typeparam name="TRequest">Command/Query istance implementing the IRequest<TResponse> interface.</typeparam>
/// <typeparam name="TResponse">The custom <see cref="Result"/> model. Represents the outcome of the action (success/failure).</typeparam>
public sealed class ValidationPipelineBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        /* The place where the array of errors that occured duriing validation of the specific Command/Query is created.
         * It is strongly linked with custom Result and Error models approach. */
        Error[] validationErrors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                $"ValidationErrors.{failure.PropertyName}",
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (validationErrors.Any())
        {
            return CreateValidationResult<TResponse>(validationErrors);
        }

        return await next();
    }

    /// <summary>
    /// Method that converts a valdiation errors array into a TReult type (<see cref="Result"/> model).
    /// It is strongly linked with custom <see cref="Result"/> and <see cref="Error"/> models approach.
    /// </summary>
    /// <typeparam name="TResult">The custom <see cref="Result"/> model. Represents the outcome of the action (success/failure).</typeparam>
    /// <param name="validationErrors">A valdiation errors array resulting from the valdiation of given Command/Query object.</param>
    private static TResult CreateValidationResult<TResult>(Error[] validationErrors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(validationErrors) as TResult)!;
        }

        object valdiationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object[] { validationErrors })!;

        return (TResult)valdiationResult;
    }
}
