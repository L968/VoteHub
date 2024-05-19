using VoteHub.Entities;

namespace VoteHub.Features.VotingSessions.GetResults;

internal sealed class GetResultsHandler(IMapper mapper) : IRequestHandler<GetResultsQuery, IEnumerable<GetResultsResponse>>
{
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetResultsResponse>> Handle(GetResultsQuery request, CancellationToken cancellationToken)
    {
        var votesQuery = @"
            SELECT participant_id,
                   COUNT(*) AS votes
            FROM vote
            WHERE session_id = ?
            GROUP BY participant_id
        ";

        var votes = await _mapper.FetchAsync<(Guid ParticipantId, int Votes)>(votesQuery, request.SessionId);
        var votesDictionary = votes.ToDictionary(v => v.ParticipantId, v => v.Votes);

        if (votesDictionary.Count == 0)
            return [];

        var participants = await _mapper.FetchAsync<Participant>("WHERE session_id = ?", request.SessionId);
        var participantsList = participants.ToList();

        return participantsList.Select(p => new GetResultsResponse
        {
            ParticipantName = p.Name,
            Votes = votesDictionary.TryGetValue(p.ParticipantId, out var votes) ? votes : 0
        });
    }
}
