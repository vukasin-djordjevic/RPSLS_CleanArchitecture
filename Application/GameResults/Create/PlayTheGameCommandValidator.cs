using Application.GameResults.Commands;
using FluentValidation;

namespace Application.GameResults.Create;

internal class PlayTheGameCommandValidator : AbstractValidator<PlayTheGameCommand>
{
    public PlayTheGameCommandValidator()
    {
        RuleFor(x => x.playersChoice).NotEmpty().LessThanOrEqualTo(5).GreaterThanOrEqualTo(1);
    }
}
