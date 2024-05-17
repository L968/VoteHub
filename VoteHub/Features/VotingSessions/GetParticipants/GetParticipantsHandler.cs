using VoteHub.Entities;

namespace VoteHub.Features.VotingSessions.GetParticipants;

internal sealed class GetParticipantsHandler(IMapper mapper) : IRequestHandler<GetParticipantsQuery, IEnumerable<GetParticipantsResponse>>
{
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetParticipantsResponse>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        var votingSessionDetails = await _mapper.FetchAsync<VotingSessionDetails>("WHERE session_id = ? ALLOW FILTERING", request.SessionId);

        return votingSessionDetails.Select(d => new GetParticipantsResponse(
            id: d.DetailsId,
            participantName: d.ParticipantName,
            imagePath: d.ParticipantImagePath
        ));
    }
}
