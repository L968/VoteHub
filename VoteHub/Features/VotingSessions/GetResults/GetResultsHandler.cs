using VoteHub.Entities;

namespace VoteHub.Features.VotingSessions.GetResults;

internal sealed class GetResultsHandler(IMapper mapper) : IRequestHandler<GetResultsQuery, IEnumerable<GetResultsResponse>>
{
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetResultsResponse>> Handle(GetResultsQuery request, CancellationToken cancellationToken)
    {
        var details = await _mapper.FetchAsync<VotingSessionDetails>("WHERE session_id = ? ALLOW FILTERING", request.SessionId);
        var detailsList = details.ToList();

        if (detailsList.Count == 0)
            return [];

        var votingSessionCounts = await _mapper.FetchAsync<VotingSessionCount>("WHERE details_id IN ?", detailsList.Select(d => d.DetailsId));

        return detailsList.Select(d =>
        {
            var count = votingSessionCounts.FirstOrDefault(c => c.DetailsId == d.DetailsId)?.VoteCount ?? 0;

            return new GetResultsResponse
            {
                ParticipantName = d.ParticipantName,
                Votes = count
            };
        });
    }
}
