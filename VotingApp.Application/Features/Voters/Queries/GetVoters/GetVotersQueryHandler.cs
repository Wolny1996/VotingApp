using VotingApp.Application.CQRS;
using VotingApp.Domain.Abstraction;
using VotingApp.Domain.Repositories;

namespace VotingApp.Application.Features.Voters.Queries.GetVoters;
internal sealed class GetVotersQueryHandler(IVotersRepository votersRepository)
        : IQueryHandler<GetVotersQuery, GetVotersResponse>
{
    private readonly IVotersRepository _votersRepository = votersRepository ?? throw new ArgumentNullException(nameof(votersRepository));

    public async Task<Result<GetVotersResponse>> Handle(
        GetVotersQuery request,
        CancellationToken cancellationToken)
    {
        var voters = await _votersRepository.GetVoters();

        var response = new GetVotersResponse(voters);
        return Result.Success(response);
    }
}