using FluentValidation;

namespace VotingApp.Application.Features.Voters.Commands.VoterVoted;
internal sealed class VoterVotedCommandValidator
    : AbstractValidator<VoterVotedCommand>
{
    public VoterVotedCommandValidator()
    {
        RuleFor(c => c.VoterId)
            .NotEmpty();
    }
}
