using System.Text.Json.Serialization;

namespace VoteHub.Features.Votes.CreateVote;

public record CreateVoteCommand : IRequest<Result>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public Guid DetailsId { get; set; }
}
