using VotingApp.Application.CQRS;
using VotingApp.Domain.Abstraction;
using VotingApp.Domain.Repositories;

namespace VotingApp.Application.Features.Candidates.Queries.GetCandidates;
internal sealed class GetCandidatesQueryHandler(ICandidatesRepository candidatesRepository)
        : IQueryHandler<GetCandidatesQuery, GetCandidatesResponse>
{
    private readonly ICandidatesRepository _candidatesRepository = candidatesRepository ?? throw new ArgumentNullException(nameof(candidatesRepository));

    public async Task<Result<GetCandidatesResponse>> Handle(
    GetCandidatesQuery request,
        CancellationToken cancellationToken)
    {
        var candidates = await _candidatesRepository.GetCandidates();

        var response = new GetCandidatesResponse(candidates);
        return Result.Success(response);
    }
}