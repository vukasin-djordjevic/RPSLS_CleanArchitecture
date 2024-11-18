using Application.Abstractions.RandomNumberService;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;


namespace Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<RandomNumberServiceSettings>()
        .BindConfiguration(RandomNumberServiceSettings.ConfigurationSection)
        .ValidateOnStart();

        var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(200 * attempt));

        services.AddHttpClient<IRandomNumberService, RandomNumberService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://codechallenge.boohma.com/random");
        })
        .AddPolicyHandler(retryPolicy)
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };
        })
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
        return services;
    }
}
