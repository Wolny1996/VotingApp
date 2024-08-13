using FluentValidation;

namespace VotingApp.Application.Features.Candidates.Commands.AddCandidate;
internal sealed class AddCandidateCommandValidator
    : AbstractValidator<AddCandidateCommand>
{
    public AddCandidateCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);
    }
}
