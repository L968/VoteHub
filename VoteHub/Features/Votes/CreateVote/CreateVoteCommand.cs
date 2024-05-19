using System.Text.Json.Serialization;

namespace VoteHub.Features.Votes.CreateVote;

public record CreateVoteCommand : IRequest<Result>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public Guid SessionId { get; set; }
    public Guid ParticipantId { get; set; }
}
