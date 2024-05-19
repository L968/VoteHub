namespace VoteHub.Entities;

public class Participant
{
    public Guid SessionId { get; set; }
    public Guid ParticipantId { get; set; }
    public string Name { get; set; } = "";
    public string ImagePath { get; set; } = "";
}
