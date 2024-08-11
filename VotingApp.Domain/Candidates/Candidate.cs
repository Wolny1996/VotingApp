using VotingApp.Domain.Abstraction;

namespace VotingApp.Domain.Candidates;
public sealed class Candidate(
    string fullName,
    int numberOfVotes = 0) : Entity()
{
    public string FullName { get; private set; } = fullName;

    public int NumberOfVotes { get; private set; } = numberOfVotes;

    public void VoteForCandidate()
    {
        NumberOfVotes++;
    }
}
