using Application.GameResults.Commands;
using FluentAssertions;

namespace Application.IntegrationTests;

public class GameResultTests : BaseIntegrationTest
{
    public GameResultTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldAddGameResult_WhenCommandIsValid()
    {
        // Arrange
        var command = new PlayTheGameCommand(1);

        // Act
        var result = await Sender.Send(command);

        // Assert
        var product = DbContext.GameResults.FirstOrDefault(p => p.Id == result.Value.id);

        Assert.NotNull(product);
    }

    [Fact]
    public async Task Create_ShouldFail_WhenCommandIsNotValid()
    {
        // Arrange
        var command = new PlayTheGameCommand(11);

        // Act
        var result = await Sender.Send(command);


        result.IsFailure.Should().BeTrue();
    }
}
