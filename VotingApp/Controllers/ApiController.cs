using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;
using System.Runtime.CompilerServices;
using VotingApp.Domain.Abstraction;
using ILogger = NLog.ILogger;

namespace VotingApp.Controllers;
[ApiController]
public class ApiController(
    ISender sender) : ControllerBase
{
    protected readonly ILogger _innerLogger = LogManager.GetCurrentClassLogger();
    /// <summary>
    /// At the time of implementing CQRS pattern,
    /// I do not need all the functionality of the MediatR library, that is <see cref="ISender"/> + <see cref="INotification"/>.
    /// </summary>
    protected readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    /// <summary>
    /// 
    /// </summary>
    protected IActionResult HandleFailure(
        Result result,
        [CallerFilePath] string callerClassPath = "",
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        var stackTrace = $"Parent:{callerClassPath}\\{callerMethodName}\\Line{callerMethodName}; Child:{result.StackTrace}";

        return result switch
        {
            /* Case for wich the data does not match. */
            { IsSuccess: true } => throw new InvalidOperationException(),

            /* Case prepared for exceptions that occurred during validation of Command/Query object properties
             * by the FluentValidation library that uses the <see cref="IValidationResult"/> interface.
             Error code always 400 - BadRequest. */
            IValidationResult validationResult => ReturnValidationFailureResponse(
                    result.Error,
                    validationResult.ValidationErrors,
                    stackTrace),

            _ => ReturnFailureResponse(
                    result.Error,
                    stackTrace,
                    result.InternalMessage),
        };
    }

    private IActionResult ReturnFailureResponse(
        Error error,
        string stackTrace,
        string? internalMessage = null)
    {
        FailureDetails response = new(
            Guid.NewGuid(),
            error.Type,
            error.Message,
            null,
            internalMessage);

        ObjectResult actionResult = new(response)
        {
            StatusCode = (int)error.HttpStatusCode,
        };

        _innerLogger.Error(
            $"Id: {response.Id};" +
            $"\r\nType: {response.Type};" +
            $"\r\nMessage: {response.Message};" +
            $"\r\nInternal message: {internalMessage};" +
            $"\r\nStack trace: {stackTrace}");

        return actionResult;
    }

    private IActionResult ReturnValidationFailureResponse(
        Error generalError,
        Error[] validationErrors,
        string stackTrace)
    {
        FailureDetails response = new(
            Guid.NewGuid(),
            generalError.Type,
            generalError.Message,
            validationErrors);

        ObjectResult actionResult = new(response)
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
        };

        var validationLog = string.Join(", ", response.ValidationErrors.Select(ve => ve.Message));

        _innerLogger.Error(
            $"Id: {response.Id};" +
            $"\r\nType: {response.Type};" +
            $"\r\nMessage: {response.Message};" +
            $"\r\nValidation errors: {validationLog};" +
            $"\r\nStack trace: {stackTrace}");

        return actionResult;
    }

    /* Custom model representing the shape of the final response for failure. */
    private record FailureDetails(
        Guid Id,
        string Type,
        string Message,
        Error[]? ValidationErrors,
        string? InternalMessage = null);
}
