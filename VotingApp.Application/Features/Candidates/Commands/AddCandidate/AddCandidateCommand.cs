using VotingApp.Application.CQRS;

namespace VotingApp.Application.Features.Candidates.Commands.AddCandidate;
public sealed record AddCandidateCommand(
    string FullName) : ICommand<int>;
