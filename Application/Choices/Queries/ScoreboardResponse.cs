namespace Application.Choices.Queries;

public sealed record ScoreboardResponse(string results, int player, int computer, DateTime created);
