using System.Reflection;

namespace VotingApp.Domain;

/// <summary>
/// A class whose purpose is to provide the <see cref="VotingApp.Domain"/> project's assembly to indicate for the architecture tests.
/// </summary>
public static class DomainAssemblyReference
{
    public static readonly Assembly Assembly = typeof(DomainAssemblyReference).Assembly;
}

