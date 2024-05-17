namespace VoteHub.Features.VotingSessions.GetParticipants;

public record GetParticipantsResponse
{
    public Guid Id { get; set; }
    public string ParticipantName { get; set; } = "";
    public string ImagePath { get; set; } = "";

    public GetParticipantsResponse(Guid id, string participantName, string imagePath)
    {
        Id = id;
        ParticipantName = participantName;
        ImagePath = imagePath;
    }
}
