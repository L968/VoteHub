namespace VoteHub.Entities;

public class VotingSession
{
    public Guid SessionId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
