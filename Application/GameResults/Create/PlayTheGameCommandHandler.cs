using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.RandomNumberService;
using Application.Data;
using Domain.GameResults;
using Domain.Shared;

namespace Application.GameResults.Commands;

internal sealed class PlayTheGameCommandHandler : ICommandHandler<PlayTheGameCommand, GameResultResponseFull>
{
    private readonly IGameResultRepository _gameResultRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRandomNumberService _randomNumberService;

    public PlayTheGameCommandHandler(
        IGameResultRepository gameResultRepository,
        IUnitOfWork unitOfWork,
        IRandomNumberService randomNumberService)
    {
        Guard.ThrowIfNull(gameResultRepository);
        Guard.ThrowIfNull(unitOfWork);
        Guard.ThrowIfNull(randomNumberService);

        _gameResultRepository = gameResultRepository;
        _unitOfWork = unitOfWork;
        _randomNumberService = randomNumberService;
    }

    public async Task<Result<GameResultResponseFull>> Handle(PlayTheGameCommand request, CancellationToken cancellationToken)
    {
        var rndNumber = await _randomNumberService.GetRandomNumberAsync(cancellationToken);

        if (rndNumber is null)
        {
            return Result.Failure<GameResultResponseFull>(new Error(
                "Choice.NotFound",
                $"Random choice could not be obtained"));
        }

        var computersChoice = RPSSLCalculations.GetChoiceByRandomNumber(rndNumber.random_number);

        var results = RPSSLCalculations.PlayTheGame(request.playersChoice, computersChoice);

        var newId = Guid.NewGuid();

        var gameResult = new GameResult
        {
            Id = newId,
            Results = results,
            Player = request.playersChoice,
            Computer = computersChoice,
            Created = DateTime.UtcNow
        };

        await _gameResultRepository.AddAsync(gameResult, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new GameResultResponseFull(newId, results, request.playersChoice, computersChoice, gameResult.Created);

        return Result.Success(response);
    }
}
