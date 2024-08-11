using VotingApp.Application.CQRS;

namespace VotingApp.Application.Features.Candidates.Queries.GetCandidates;
public sealed record GetCandidatesQuery(
    ) : IQuery<GetCandidatesResponse>;
