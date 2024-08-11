using VotingApp.Domain.Abstraction;

namespace VotingApp.Tests.Features;
/// <summary>
/// Class responsible for validating result status.
/// Strongly related to the <see cref="Result"/> and <see cref="Error"/> model approach.
/// </summary>
internal static class ResultChecker
{
    public static void CheckSuccessResult(Result result)
    {
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }

    public static void CheckFailureResult(Result result, Error expectedResult)
    {
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(expectedResult);
    }
}
