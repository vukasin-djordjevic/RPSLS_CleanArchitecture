using Application.Abstractions.Messaging;
using Domain.Shared;

namespace Application.Choices.Queries.GetAllChoices;

internal sealed class GetAllChoicesQueryHandler
    : IQueryHandler<GetAllChoicesQuery, List<ChoiceResponse>>
{
    public async Task<Result<List<ChoiceResponse>>> Handle(
        GetAllChoicesQuery query,
        CancellationToken cancellationToken)
    {
        List<ChoiceResponse> choicesList = new List<ChoiceResponse>();

        // This query handler intentionally does not use the database - that seemed
        // to me as unnecessary roundtrip, since we have only five choices.
        // In real business case, of course that database would be used.
        for (int i = 1; i <= 5; i++)
        {
            choicesList.Add(new ChoiceResponse(i, ((Domain.Enums.Choices)i).ToString()));
        }

        // This Task.FromResult was needed since
        // IQueryHandler.Handle method is asynchronous
        return await Task.FromResult(choicesList);
    }
}
