namespace VoteHub.Features.VotingSessions.GetParticipants;

public record GetParticipantsQuery(Guid SessionId) : IRequest<IEnumerable<GetParticipantsResponse>>
{
}
