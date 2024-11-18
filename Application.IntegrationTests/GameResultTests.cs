using Application.GameResults.Commands;

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
}
