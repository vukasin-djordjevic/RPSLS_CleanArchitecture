using Application.Abstractions.Messaging;
using Domain.GameResults;
using Domain.Shared;

namespace Application.Choices.Queries.GetScoreboard;

internal sealed class GetScoreboardQueryHandler
: IQueryHandler<GetScoreboardQuery, List<ScoreboardResponse>>
{
    private readonly IGameResultRepository _gameResultRepository;

    public GetScoreboardQueryHandler(IGameResultRepository gameResultRepository)
    {
        _gameResultRepository = gameResultRepository;
    }

    public async Task<Result<List<ScoreboardResponse>>> Handle(GetScoreboardQuery request, CancellationToken cancellationToken)
    {
        var scoreboard = await _gameResultRepository.GetScoreboardAsync(request.count, cancellationToken);

        List<ScoreboardResponse> responseList = new List<ScoreboardResponse>();

        foreach (var position in scoreboard)
        {
            responseList.Add(new ScoreboardResponse(position.Results, position.Player, position.Computer, position.Created));
        }

        return responseList;
    }
}
