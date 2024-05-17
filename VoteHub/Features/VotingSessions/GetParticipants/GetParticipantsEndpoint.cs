namespace VoteHub.Features.VotingSessions.GetParticipants;

public class GetParticipantsEndpoint : ICarterModule
{
    private static async Task<IResult> HandleAsync(Guid sessionId, ISender sender)
    {
        return Results.Ok(await sender.Send(new GetParticipantsQuery(sessionId)));
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("session/{sessionId}/participants", HandleAsync);
    }
}
