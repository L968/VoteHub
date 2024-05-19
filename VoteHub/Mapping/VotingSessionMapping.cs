using VoteHub.Entities;

namespace VoteHub.Mapping;

public class VotingSessionMapping : Mappings
{
    public VotingSessionMapping()
    {
        For<VotingSession>()
            .TableName("voting_session")
            .PartitionKey(vs => vs.SessionId)
            .Column(vs => vs.SessionId, cm => cm.WithName("session_id"))
            .Column(vs => vs.StartTime, cm => cm.WithName("start_time"))
            .Column(vs => vs.EndTime, cm => cm.WithName("end_time"));
    }
}
