using VoteHub.Entities;

namespace VoteHub.Mapping;

public class ParticipantMapping : Mappings
{
    public ParticipantMapping()
    {
        For<Participant>()
            .TableName("participant")
            .PartitionKey(p => p.SessionId)
            .ClusteringKey(p => p.ParticipantId)
            .Column(p => p.SessionId, cm => cm.WithName("session_id"))
            .Column(p => p.ParticipantId, cm => cm.WithName("participant_id"))
            .Column(p => p.Name, cm => cm.WithName("name"))
            .Column(p => p.ImagePath, cm => cm.WithName("image_path"));
    }
}
