using VotingApp.Application.CQRS;
using VotingApp.Domain.Abstraction;
using VotingApp.Domain.Candidates;
using VotingApp.Domain.Repositories;

namespace VotingApp.Application.Features.Candidates.Commands.AddCandidate;
internal sealed class AddCandidateCommandHandler(ICandidatesRepository candidatesRepository)
        : ICommandHandler<AddCandidateCommand, int>
{
    private readonly ICandidatesRepository _candidatesRepository = candidatesRepository ?? throw new ArgumentNullException(nameof(candidatesRepository));

    public async Task<Result<int>> Handle(
        AddCandidateCommand request,
        CancellationToken cancellationToken)
    {
        var candidate = new Candidate(request.FullName);

        await _candidatesRepository.Add(candidate);
        await _candidatesRepository.Save(cancellationToken);

        return Result.Success(candidate.Id);
    }
}