using Application.Abstractions;
using Application.Abstractions.RandomNumberService;
using Application.Data;
using Application.GameResults.Commands;
using Domain.GameResults;
using Domain.Shared;
using FluentAssertions;
using Infrastructure.Services;
using NSubstitute;

namespace Application.UnitTests;

public class PlayTheGameCommandTest
{ 

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenGameSucceeds()
    {
        // Arrange

        IGameResultRepository _gameResultRepositoryMock = Substitute.For<IGameResultRepository>();
        IUnitOfWork _unitOfWorkMock = _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://codechallenge.boohma.com/random");
        IRandomNumberService _randomNumberServiceMock = new RandomNumberService(httpClient);

        PlayTheGameCommandHandler _handler = new PlayTheGameCommandHandler(
            _gameResultRepositoryMock,
            _unitOfWorkMock,
            _randomNumberServiceMock);

        PlayTheGameCommand Command = new(1);

        // Act
        Result<GameResultResponseFull> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
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

        //Assert
        Assert.Equal(expectedResult, results);
    }
}