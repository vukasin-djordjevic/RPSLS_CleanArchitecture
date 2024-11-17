using Application.Abstractions.RandomNumberService;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public sealed class RandomNumberService:IRandomNumberService
    {
        private readonly HttpClient _client;

        public RandomNumberService(HttpClient client)
        {
            _client = client;
        }
    public async Task<RandomNumberServiceResponse> GetRandomNumberAsync(CancellationToken cancellationToken = default) 
        {
            var content = await _client.GetFromJsonAsync<RandomNumberServiceResponse>(string.Empty,cancellationToken);
            return content; 
        }
    }
}
