namespace Application.Abstractions.RandomNumberService
{
    public interface IRandomNumberService
    {
        Task<RandomNumberServiceResponse> GetRandomNumberAsync(CancellationToken cancellationToken = default);
    }
}