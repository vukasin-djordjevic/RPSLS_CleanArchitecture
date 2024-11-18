using Application.Abstractions.Messaging;

namespace Application.GameResults.Commands;

public sealed record PlayTheGameCommand(int playersChoice) : ICommand<GameResultResponseFull>;
