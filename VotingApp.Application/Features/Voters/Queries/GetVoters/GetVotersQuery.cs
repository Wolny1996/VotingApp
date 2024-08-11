using VotingApp.Application.CQRS;

namespace VotingApp.Application.Features.Voters.Queries.GetVoters;
public sealed record GetVotersQuery(
    ) : IQuery<GetVotersResponse>;
