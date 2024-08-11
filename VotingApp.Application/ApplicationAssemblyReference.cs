using System.Reflection;

namespace VotingApp.Application;

/// <summary>
/// A class whose purpose is to provide the <see cref="VotingApp.Application"/> project's assembly to indicate in the Startup.cs file
/// for MediatR and FluentValidation libraries. Also it is used for architecture tests.
/// </summary>
public static class ApplicationAssemblyReference
{
    public static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
