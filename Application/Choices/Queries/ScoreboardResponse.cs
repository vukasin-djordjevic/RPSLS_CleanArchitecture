using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Choices.Queries
{
    public sealed record ScoreboardResponse(string results, int player, int computer, DateTime created);
}
