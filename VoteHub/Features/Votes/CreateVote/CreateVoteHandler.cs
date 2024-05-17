using VoteHub.Entities;

namespace VoteHub.Features.Votes.CreateVote;

internal sealed class CreateVoteHandler(IMapper mapper) : IRequestHandler<CreateVoteCommand, Result>
{
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        VotingSessionDetails votingSessionDetails = await _mapper.SingleOrDefaultAsync<VotingSessionDetails>("WHERE details_id = ?", request.DetailsId);

        if (votingSessionDetails.StartTime > DateTime.UtcNow || votingSessionDetails.EndTime < DateTime.UtcNow)
        {
            return Result.Fail("Voting session is not active");
        }

        await _mapper.UpdateAsync<VotingSessionCount>("SET vote_count = vote_count + 1 WHERE details_id = ?", request.DetailsId);

        return Result.Ok();
    }
}
