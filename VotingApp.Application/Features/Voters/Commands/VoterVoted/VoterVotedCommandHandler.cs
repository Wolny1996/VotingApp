using VotingApp.Application.CQRS;
using VotingApp.Domain.Abstraction;
using VotingApp.Domain.Candidates;
using VotingApp.Domain.Repositories;
using VotingApp.Domain.Voters;

namespace VotingApp.Application.Features.Voters.Commands.VoterVoted;
internal sealed class VoterVotedCommandHandler(
    ICandidatesRepository candidatesRepository,
    IVotersRepository votersRepository)
        : ICommandHandler<VoterVotedCommand>
{
    private readonly ICandidatesRepository _candidatesRepository = candidatesRepository ?? throw new ArgumentNullException(nameof(candidatesRepository));
    private readonly IVotersRepository _votersRepository = votersRepository ?? throw new ArgumentNullException(nameof(votersRepository));

    public async Task<Result> Handle(
        VoterVotedCommand request,
        CancellationToken cancellationToken)
    {
        Voter voter = await _votersRepository.GetVoterBy(request.VoterId);
        if (voter == null)
        {
            return Result.Failure(
                VotersErrors.NotFound,
                $"Incoming parameters: voter id: {request.VoterId}");
        }

        if (voter.HasVoted)
        {
            return Result.Failure(
                VotersErrors.AlreadyCastVote,
                $"Incoming parameters: voter id: {request.VoterId}");
        }

        Candidate candidate = await _candidatesRepository.GetCandidateBy(request.CandidateId);
        if (candidate == null)
        {
            return Result.Failure(
                CandidatesErrros.NotFound,
                $"Incoming parameters: candidate id: {request.CandidateId}");
        }

        voter.Vote();
        candidate.VoteForCandidate();

        await _votersRepository.Save(cancellationToken);
        return Result.Success();
    }
}