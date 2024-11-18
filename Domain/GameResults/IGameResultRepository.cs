namespace Domain.GameResults;

public interface IGameResultRepository
{
    Task<List<GameResult>> GetScoreboardAsync(int count, CancellationToken cancellationToken = default);
    Task AddAsync(GameResult gameResult, CancellationToken cancellationToken = default);
    Task DeleteAllAsync(CancellationToken cancellationToken = default);
}
