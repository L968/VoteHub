namespace VoteHub.Features.VotingSessions.GetResults;

public record GetResultsQuery(Guid SessionId) : IRequest<IEnumerable<GetResultsResponse>>
{
}
