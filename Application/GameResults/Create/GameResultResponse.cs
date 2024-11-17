
namespace Application.GameResults.Commands
{
    public sealed record GameResultResponse(Guid id, string results, int player, int computer);
}
