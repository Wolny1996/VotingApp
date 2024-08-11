using VotingApp.Application.CQRS;

namespace VotingApp.Application.Features.Voters.Commands.AddVoter;
public sealed record AddVoterCommand(
    string FullName) : ICommand<int>;
