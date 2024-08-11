using VotingApp.Domain.Abstraction;

namespace VotingApp.Domain.Voters;
public sealed class Voter(
    string fullName, bool hasVoted = false) : Entity()
{
    public string FullName { get; private set; } = fullName;

    public bool HasVoted { get; private set; } = hasVoted;

    public void Vote()
    {
        HasVoted = true;
    }
}
