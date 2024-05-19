using Microsoft.AspNetCore.Authorization;

namespace VoteHub.Features.VotingSessions.GetResults;

public class GetResultsEndpoint : ICarterModule
{
    [Authorize(Roles = "regular")]
    private static async Task<IResult> HandleAsync(Guid sessionId, ISender sender)
    {
        return Results.Ok(await sender.Send(new GetResultsQuery(sessionId)));
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("session/{sessionId}/result", HandleAsync);
    }
}
