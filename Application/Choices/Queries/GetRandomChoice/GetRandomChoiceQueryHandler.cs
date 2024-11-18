using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.RandomNumberService;
using Domain.Shared;

namespace Application.Choices.Queries.GetRandomChoice;

internal sealed class GetRandomChoiceQueryHandler
    : IQueryHandler<GetRandomChoiceQuery, ChoiceResponse>
{
    private readonly IRandomNumberService _randomNumberService;
    public GetRandomChoiceQueryHandler(
        IRandomNumberService randomNumberService)
    {
        Guard.ThrowIfNull(randomNumberService);
        _randomNumberService = randomNumberService;
    }

    public async Task<Result<ChoiceResponse>> Handle(
        GetRandomChoiceQuery query,
        CancellationToken cancellationToken)
    {
        var rndNumber = await _randomNumberService.GetRandomNumberAsync(cancellationToken);


        if (rndNumber is null)
        {
            return Result.Failure<ChoiceResponse>(new Error(
                "Choice.NotFound",
                $"Random choice could not be obtained"));
        }

        var choiceId = RPSSLCalculations.GetChoiceByRandomNumber(rndNumber.random_number);

        var response = new ChoiceResponse(choiceId, ((Domain.Enums.Choices)choiceId).ToString());

        return response;
    }
}
