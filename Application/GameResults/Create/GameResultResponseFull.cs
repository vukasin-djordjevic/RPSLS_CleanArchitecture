
namespace Application.GameResults.Commands;

public sealed record GameResultResponseFull(Guid id, string results, int player, int computer, DateTime created);
