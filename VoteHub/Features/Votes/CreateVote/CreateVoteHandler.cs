using VoteHub.Entities;

namespace VoteHub.Features.Votes.CreateVote;

internal sealed class CreateVoteHandler(IMapper mapper, ILogger<CreateVoteHandler> logger) : IRequestHandler<CreateVoteCommand, Result>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CreateVoteHandler> _logger = logger;

    public async Task<Result> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        var session = await _mapper.SingleOrDefaultAsync<VotingSession>("WHERE session_id = ?", request.SessionId);

        if (session.StartTime > DateTime.UtcNow || session.EndTime < DateTime.UtcNow)
        {
            var message = "Voting session is not active";
            _logger.LogWarning(message);
            return Result.Fail(message);
        }

        var vote = new Vote(request.SessionId, request.ParticipantId, request.UserId);
        await _mapper.InsertAsync(vote);

        DiagnosticsConfig.VotesCounter.Add(1, new KeyValuePair<string, object?>("voting.participant_id", request.ParticipantId));
        _logger.LogInformation("Successfully create {@Vote}", request);

        return Result.Ok();
    }
}
