namespace VotingApp.Application.Features.Voters.Commands.VoterVoted;

public sealed record VoterVotedRequest(
    int CandidateId,
    int VoterId);
