using VotingApp.Application.CQRS;
using VotingApp.Domain.Abstraction;
using VotingApp.Domain.Repositories;
using VotingApp.Domain.Voters;

namespace VotingApp.Application.Features.Voters.Commands.AddVoter;
internal sealed class AddVoterCommandHandler(IVotersRepository votersRepository)
        : ICommandHandler<AddVoterCommand, int>
{
    private readonly IVotersRepository _votersRepository = votersRepository ?? throw new ArgumentNullException(nameof(votersRepository));

    public async Task<Result<int>> Handle(
        AddVoterCommand request,
        CancellationToken cancellationToken)
    {
        var voter = new Voter(request.FullName);

        await _votersRepository.Add(voter);
        await _votersRepository.Save(cancellationToken);

        return Result.Success(voter.Id);
    }
}