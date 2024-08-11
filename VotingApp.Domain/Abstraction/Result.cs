using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VotingApp.Domain.Abstraction;

/// <summary>
/// Result model is way to express result of an action.
/// It describes two states of action outcome: Success and Failure.
/// Its aim to replace the approach of throwing out exceptions in expected situations (when we know how to deal with them).
/// It is strongly linked with custom <see cref="Abstraction.Error"/> model approach.
/// </summary>
public class Result
{
    protected internal Result(
        bool isSuccess,
        Error error,
        string? internalMessage = null,
        string? stackTrace = null)
    {
        /* 2 scenarios for which the data in the Result and Error models are mutually exclusive. */
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
        InternalMessage = internalMessage;
        StackTrace = stackTrace;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The property representing the (expected) error that prevented the action from being performed.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Adds additional information about the problem like incoming parameters (ex. identifier).
    /// Allows additional information to be passed from the code.
    /// Used by the <see cref="Result.Failure"/> to pass additional information from the code.
    /// </summary>
    public string? InternalMessage { get; }

    /// <summary>
    /// Path locating where the failure method was called.
    /// Its purpose is to speed up the localization of where the error occurred.
    /// </summary>
    public string? StackTrace { get; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);


    public static Result Failure(
        Error error,
        string? internalMessage,
        [CallerFilePath] string callerClassPath = "",
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0)
            => new(false, error, internalMessage, SetStackTrace(callerClassPath, callerMethodName, callerLineNumber));

    public static Result<TValue> Failure<TValue>(
        Error error,
        string? internalMessage,
        [CallerFilePath] string callerClassPath = "",
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0)
            => new(default, false, error, internalMessage, SetStackTrace(callerClassPath, callerMethodName, callerLineNumber));

    /// <summary>
    /// These 2 methoods should be used when we pass the Result from the method below
    /// checking if the result of the action is a failure.
    /// This ensures that the <see cref="Result.InternalMessage"/> as well as the <see cref="Result.StackTrace"/> is not lost/overwritten.
    /// </summary>
    public static Result Failure(
    Result result)
        => new(false, result.Error, result.InternalMessage, result.StackTrace);

    public static Result<TValue> Failure<TValue>(
        Result result)
            => new(default, false, result.Error, result.InternalMessage, result.StackTrace);

    public static Result<TValue> Create<TValue>(TValue? value) => value is not null
        ? Success(value)
        : Failure<TValue>(Error.NullValue);

    private static string SetStackTrace(
        string callerClassPath,
        string callerMethodName,
        int callerLineNumber)
    {
        return $"{callerClassPath}\\{callerMethodName}\\Line{callerMethodName}";
    }
}

/// <summary>
/// Generic version of <see cref="Result"/> model.
/// </summary>
/// <typeparam name="TValue">The generic type of <see cref="Result"/> model.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(
        TValue? value,
        bool isSuccess,
        Error error,
        string? internalMessage = null,
        string? stackTrace = null)
        : base(isSuccess, error, internalMessage, stackTrace)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}