using VoteHub.Entities;

namespace VoteHub.Mapping;

public class VotingSessionCountMapping : Mappings
{
    public VotingSessionCountMapping()
    {
        For<VotingSessionCount>()
            .TableName("voting_session_count")
            .PartitionKey(v => v.DetailsId)
            .Column(v => v.DetailsId, cm => cm.WithName("details_id"))
            .Column(v => v.VoteCount, cm => cm.WithName("vote_count"));
    }
}
