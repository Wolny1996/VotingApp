using VotingApp.Domain.Candidates;
using VotingApp.Domain.Voters;

namespace VotingApp.Domain.Context;
public static class DataSeed
{
    public static void Seed(VotingAppContext context)
    {
        context.Candidates.AddRange(
            new Candidate("Kyrre Bastion"),
            new Candidate("Fiona Inferno")
        );

        context.Voters.AddRange(
            new Voter("Bron Fortress "),
            new Voter("Crag Hack Stronghold")
        );

        context.SaveChanges();
    }
}
