using FluentValidation;

namespace VotingApp.Application.Features.Voters.Commands.AddVoter;
internal sealed class AddVoterCommandValidator
    : AbstractValidator<AddVoterCommand>
{
    public AddVoterCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
