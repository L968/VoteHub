using VoteHub.Entities;

namespace VoteHub.Features.VotingSessions.GetParticipants;

internal sealed class GetParticipantsHandler(IMapper mapper) : IRequestHandler<GetParticipantsQuery, IEnumerable<GetParticipantsResponse>>
{
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetParticipantsResponse>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        var participants = await _mapper.FetchAsync<Participant>("WHERE session_id = ?", request.SessionId);

        return participants.Select(p => new GetParticipantsResponse(
            participantId: p.ParticipantId,
            name: p.Name,
            imagePath: p.ImagePath
        ));
    }
}
