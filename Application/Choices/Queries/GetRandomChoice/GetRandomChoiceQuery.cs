using Application.Abstractions.Messaging;

namespace Application.Choices.Queries.GetRandomChoice;
    public sealed record GetRandomChoiceQuery() : IQuery<ChoiceResponse>;
}
