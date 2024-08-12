using VotingApp.Domain.Candidates;
using VotingApp.Domain.Voters;

namespace VotingApp.Domain.Context;
public static class DataSeed
{
    public static void Seed(VotingAppContext context)
    {
        context.Candidates.AddRange(
            new Candidate("C1"),
            new Candidate("C2")
        );

        context.Voters.AddRange(
            new Voter("V1"),
            new Voter("V2")
        );

        context.SaveChanges();
    }
}
