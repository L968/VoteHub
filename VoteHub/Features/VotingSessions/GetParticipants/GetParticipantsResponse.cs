namespace VoteHub.Features.VotingSessions.GetParticipants;

public record GetParticipantsResponse
{
    public Guid ParticipantId { get; set; }
    public string Name { get; set; } = "";
    public string ImagePath { get; set; } = "";

    public GetParticipantsResponse(Guid participantId, string name, string imagePath)
    {
        ParticipantId = participantId;
        Name = name;
        ImagePath = imagePath;
    }
}
