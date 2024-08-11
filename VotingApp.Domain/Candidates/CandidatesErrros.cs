using System.Net;
using VotingApp.Domain.Abstraction;

namespace VotingApp.Domain.Candidates;
public static class CandidatesErrros
{
    public static readonly Error NotFound = new(
        "Candidates.NotFound",
        "Candidate has not been found.",
        HttpStatusCode.NotFound);
}
