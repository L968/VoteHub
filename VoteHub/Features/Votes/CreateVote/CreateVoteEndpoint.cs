using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace VoteHub.Features.Votes.CreateVote;

public class CreateVoteEndpoint : ICarterModule
{
    [Authorize(Roles = "regular")]
    private static async Task<IResult> HandleAsync(CreateVoteCommand command, ISender sender, ClaimsPrincipal user)
    {
        command.UserId = int.Parse(user.FindFirstValue("id")!);

        var result = await sender.Send(command);

        if (result.IsFailed)
            return Results.BadRequest(result.Reasons);

        return Results.NoContent();
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("vote", HandleAsync);
    }
}
