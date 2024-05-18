﻿using VoteHub.Entities;

namespace VoteHub.Features.Votes.CreateVote;

internal sealed class CreateVoteHandler(IMapper mapper, ILogger<CreateVoteHandler> logger) : IRequestHandler<CreateVoteCommand, Result>
{
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CreateVoteHandler> _logger = logger;

    public async Task<Result> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        VotingSessionDetails votingSessionDetails = await _mapper.SingleOrDefaultAsync<VotingSessionDetails>("WHERE details_id = ?", request.DetailsId);

        if (votingSessionDetails.StartTime > DateTime.UtcNow || votingSessionDetails.EndTime < DateTime.UtcNow)
        {
            var message = "Voting session is not active";
            _logger.LogWarning(message);
            return Result.Fail(message);
        }

        await _mapper.UpdateAsync<VotingSessionCount>("SET vote_count = vote_count + 1 WHERE details_id = ?", request.DetailsId);

        DiagnosticsConfig.VotesCounter.Add(1, new KeyValuePair<string, object?>("voting.participant_name", votingSessionDetails.ParticipantName));
        _logger.LogInformation("Successfully create {@Vote}", request);

        return Result.Ok();
    }
}
