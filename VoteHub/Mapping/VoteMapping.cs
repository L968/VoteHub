using VoteHub.Entities;

namespace VoteHub.Mapping;

public class VoteMapping : Mappings
{
    public VoteMapping()
    {
        For<Vote>()
            .TableName("vote")
            .PartitionKey(v => v.SessionId)
            .ClusteringKey(v => v.ParticipantId)
            .ClusteringKey(v => v.VoteId)
            .Column(v => v.VoteId, cm => cm.WithName("vote_id"))
            .Column(v => v.SessionId, cm => cm.WithName("session_id"))
            .Column(v => v.ParticipantId, cm => cm.WithName("participant_id"))
            .Column(v => v.UserId, cm => cm.WithName("user_id"))
            .Column(v => v.Timestamp, cm => cm.WithName("timestamp"));
    }
}
