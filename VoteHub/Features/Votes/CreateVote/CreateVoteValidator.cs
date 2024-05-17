namespace VoteHub.Features.Votes.CreateVote;

public class CreateVoteValidator : AbstractValidator<CreateVoteCommand>
{
    public CreateVoteValidator()
    {
        RuleFor(c => c.DetailsId).NotEmpty();
    }
}
