using VotingApp.Domain.Candidates;

namespace VotingApp.Application.Features.Candidates.Queries.GetCandidates;

public sealed record GetCandidatesResponse(
    Candidate[] Candidates);
