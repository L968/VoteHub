namespace VoteHub.Features.VotingSessions.GetResults;

public record GetResultsResponse
{
    public string ParticipantName { get; set; } = "";
    public int Votes { get; set; }
}
