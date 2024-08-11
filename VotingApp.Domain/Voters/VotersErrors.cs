using System.Net;
using VotingApp.Domain.Abstraction;

namespace VotingApp.Domain.Voters;
public static class VotersErrors
{
    public static readonly Error NotFound = new(
        "Voters.NotFound",
        "Voter has not been found.",
        HttpStatusCode.NotFound);

    public static readonly Error AlreadyCastVote = new(
        "Voters.AlreadyCastVote",
        "Voter already cast their vote.");
}
