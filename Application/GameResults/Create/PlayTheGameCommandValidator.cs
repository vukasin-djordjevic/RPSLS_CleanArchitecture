using Application.GameResults.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GameResults.Create
{
    internal class PlayTheGameCommandValidator : AbstractValidator<PlayTheGameCommand>
    {
        public PlayTheGameCommandValidator()
        {
            RuleFor(x => x.playersChoice).NotEmpty().LessThanOrEqualTo(5).GreaterThanOrEqualTo(1);
        }
    }
}
