using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.RandomNumberService;
using Application.Data;
using Domain.GameResults;
using Domain.Shared;

namespace Application.GameResults.Commands
{
    internal sealed class PlayTheGameCommandHandler : ICommandHandler<PlayTheGameCommand, GameResultResponse>
    {
        private readonly IGameResultRepository _gameResultRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRandomNumberService _randomNumberService;

        public PlayTheGameCommandHandler(
            IGameResultRepository gameResultRepository,
            IUnitOfWork unitOfWork,
            IRandomNumberService randomNumberService)
        {
            _gameResultRepository = gameResultRepository;
            _unitOfWork = unitOfWork;
            _randomNumberService = randomNumberService;
    }

        public async Task<Result<GameResultResponse>> Handle(PlayTheGameCommand request, CancellationToken cancellationToken)
        {
            var rndNumber = await _randomNumberService.GetRandomNumberAsync(cancellationToken);

            if (request.playersChoice < 1 || request.playersChoice > 5)
            {
                return Result.Failure<GameResultResponse>(new Error(
                    "Choice.Invalid",
                    $"Choice must be an integer between 1 and 5"));
            }

            if (rndNumber is null)
            {
                return Result.Failure<GameResultResponse>(new Error(
                    "Choice.NotFound",
                    $"Random choice could not be obtained"));
            }

            var computersChoice = RPSSLCalculations.GetChoiceByRandomNumber(rndNumber.random_number);

            var results = RPSSLCalculations.PlayTheGame(request.playersChoice, computersChoice);

            var newId = Guid.NewGuid();

            var gameResult = new GameResult { 
                Id = newId,
                Results = results,
                Player = request.playersChoice,
                Computer = computersChoice,
                Created = DateTime.UtcNow
            };

            await _gameResultRepository.AddAsync(gameResult, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new GameResultResponse(newId, results, request.playersChoice, computersChoice);

            return Result.Success(response);
        }
    }
}
