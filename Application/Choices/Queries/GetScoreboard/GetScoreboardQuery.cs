using Application.Abstractions.Messaging;

namespace Application.Choices.Queries.GetScoreboard;

public record GetScoreboardQuery(int count) : IQuery<List<ScoreboardResponse>>;
