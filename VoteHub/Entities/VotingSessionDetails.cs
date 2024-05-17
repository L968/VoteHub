namespace VoteHub.Entities;

public class VotingSessionDetails
{
    public Guid DetailsId { get; set; }
    public Guid SessionId { get; set; }
    public string ParticipantName { get; set; } = "";
    public string ParticipantImagePath { get; set; } = "";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
