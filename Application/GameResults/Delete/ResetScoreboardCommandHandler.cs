using Application.Data;
using Domain.GameResults;
using MediatR;

namespace Application.GameResults.Delete
{
    internal sealed class ResetScoreboardCommandHandler : IRequestHandler<ResetScoreboardCommand>
    {
        private readonly IGameResultRepository _gameResultRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResetScoreboardCommandHandler(IGameResultRepository gameResultRepository, IUnitOfWork unitOfWork)
        {
            _gameResultRepository = gameResultRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ResetScoreboardCommand request, CancellationToken cancellationToken)
        {
            await _gameResultRepository.DeleteAllAsync(cancellationToken);
        }
    }
}
