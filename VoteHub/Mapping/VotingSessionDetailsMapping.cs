using VoteHub.Entities;

namespace VoteHub.Mapping;

public class VotingSessionDetailsMapping : Mappings
{
    public VotingSessionDetailsMapping()
    {
        For<VotingSessionDetails>()
            .TableName("voting_session_details")
            .PartitionKey(v => v.DetailsId)
            .Column(v => v.DetailsId, cm => cm.WithName("details_id"))
            .Column(v => v.SessionId, cm => cm.WithName("session_id"))
            .Column(v => v.ParticipantName, cm => cm.WithName("participant_name"))
            .Column(v => v.ParticipantImagePath, cm => cm.WithName("participant_image_path"))
            .Column(v => v.StartTime, cm => cm.WithName("start_time"))
            .Column(v => v.EndTime, cm => cm.WithName("end_time"));
    }
}
