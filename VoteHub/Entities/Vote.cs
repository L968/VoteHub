namespace VoteHub.Entities;

public class Vote
{
    public Guid VoteId { get; private set; }
    public Guid SessionId { get; private set; }
    public Guid ParticipantId { get; private set; }
    public int UserId { get; private set; }
    public DateTime Timestamp { get; private set; }

    public Vote(Guid sessionId, Guid participantId, int userId)
    {
        VoteId = Guid.NewGuid();
        SessionId = sessionId;
        ParticipantId = participantId;
        UserId = userId;
        Timestamp = DateTime.UtcNow;
    }
}
