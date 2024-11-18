using Application.Abstractions.Messaging;

namespace Application.Choices.Queries.GetAllChoices;

public sealed record GetAllChoicesQuery() : IQuery<List<ChoiceResponse>>;
