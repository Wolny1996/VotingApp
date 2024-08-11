using VotingApp.Domain.Voters;

namespace VotingApp.Application.Features.Voters.Queries.GetVoters;

public sealed record GetVotersResponse(
    Voter[] Voters);
