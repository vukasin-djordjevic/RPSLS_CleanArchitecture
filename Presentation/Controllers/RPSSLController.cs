using Application.Choices.Queries;
using Application.Choices.Queries.GetAllChoices;
using Application.Choices.Queries.GetRandomChoice;
using Application.Choices.Queries.GetScoreboard;
using Application.GameResults.Commands;
using Application.GameResults.Delete;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/RPSSL")]
public sealed class RPSSLController : ApiController
{
    public RPSSLController(ISender sender, IConfiguration configuration)
        : base(sender)
    {
    }

    [SwaggerOperation(Summary = "Runs a new R-P-S-S-L game against computer")]
    [SwaggerResponse(200, Description = @"Returns JSON: { ""results"": string [12] (win, lose, tie), ""player"": integer [1-5], ""computer"": integer [1-5] }")]
    [HttpPost("V1/play")]
    public async Task<IActionResult> Play(int player, CancellationToken cancellationToken)
    {
        var command = new PlayTheGameCommand(player);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(new GameResultResponse(result.Value.results, result.Value.player, result.Value.computer)) : HandleFailure(result);
    }

    [SwaggerOperation(Summary = "Returns a random choice")]
    [SwaggerResponse(200, Description = @"Returns a JSON: { ""id"": integer [1-5], ""name"": string [12] (rock, paper, scissors, lizard, spock) }")]
    [HttpGet("V1/choice")]
    public async Task<IActionResult> GetRandomChoice()
    {
        var query = new GetRandomChoiceQuery();

        Result<ChoiceResponse> response = await Sender.Send(query);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [SwaggerOperation(Summary = "Returns all available choices")]
    [SwaggerResponse(200, Description = @"Returns JSON: [ { ""id"": integer[1 - 5], ""name"": string [12] (rock, paper, scissors, lizard, spock) } ]")]
    [HttpGet("V1/choices")]
    public async Task<IActionResult> GetAllChoices()
    {

        var query = new GetAllChoicesQuery();

        Result<List<ChoiceResponse>> response = await Sender.Send(query);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [SwaggerOperation(Summary = "Returns not more than last ten game results")]
    [SwaggerResponse(200, Description = @"Returns not more than last ten game results in JSON format:   [ { ""results"": string [12] (win, lose, tie), ""player"": integer [1-5], ""computer"": integer [1-5], ""created"": datetime } ]")]
    [HttpGet("V1/scoreboard")]
    public async Task<IActionResult> GetScoreboard()
    {
        var count = int.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["scoreboard_count"]!);

        var query = new GetScoreboardQuery(count);

        Result<List<ScoreboardResponse>> response = await Sender.Send(query);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [SwaggerOperation(Summary = "Deletes ALL the game results from the database")]
    [HttpDelete("V1/resetScoreboard")]
    public async Task<IActionResult> ResetScoreboard()
    {
        await Sender.Send(new ResetScoreboardCommand());

        return Ok();
    }
}