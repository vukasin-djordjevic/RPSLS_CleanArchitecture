using Application.Abstractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Choices.Queries.GetScoreboard
{
    public record GetScoreboardQuery(int count) : IQuery<List<ScoreboardResponse>> 
    {
    }
}
