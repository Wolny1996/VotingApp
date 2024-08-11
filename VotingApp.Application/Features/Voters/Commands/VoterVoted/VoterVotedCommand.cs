using VotingApp.Application.CQRS;

namespace VotingApp.Application.Features.Voters.Commands.VoterVoted;
public sealed record VoterVotedCommand(
    int CandidateId,
    int VoterId) : ICommand;
