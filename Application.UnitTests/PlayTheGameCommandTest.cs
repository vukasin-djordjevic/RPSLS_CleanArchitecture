using Application.Abstractions;
using Application.Abstractions.RandomNumberService;
using Application.Data;
using Application.GameResults.Commands;
using Domain.GameResults;
using Domain.Shared;
using FluentAssertions;
using Infrastructure.Services;
using NSubstitute;

namespace Application.UnitTests
{
    public class PlayTheGameCommandTest
    {
        private readonly PlayTheGameCommandHandler _handler;
        private readonly IGameResultRepository _gameResultRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IRandomNumberService _randomNumberServiceMock;

        public PlayTheGameCommandTest()
        {
            _gameResultRepositoryMock = Substitute.For<IGameResultRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://codechallenge.boohma.com/random");
            _randomNumberServiceMock = new RandomNumberService(httpClient);

            _handler = new PlayTheGameCommandHandler(
                _gameResultRepositoryMock,
                _unitOfWorkMock,
                _randomNumberServiceMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenGameSucceeds()
        {
            // Arrange
            PlayTheGameCommand Command = new(1);

            // Act
            Result<GameResultResponse> result = await _handler.Handle(Command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenGameFails()
        {
            // Arrange
            PlayTheGameCommand Command = new(11);

            // Act
            Result<GameResultResponse> result = await _handler.Handle(Command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenSpockLoseFromLizzard()
        {
            // Arrange
            var player = 5;
            var computer = 4;
            var expectedResult = "lose";

            // Act

            var results = await Task.FromResult(RPSSLCalculations.PlayTheGame(player, computer));

            Assert.Equal(expectedResult, results);
        }
    }
}