using VotingApp.Domain.Candidates;
using VotingApp.Domain.Context;
using VotingApp.Domain.Voters;

namespace VotingApp.Tests.Features.Voters;
internal static class VotersDatabaseData
{
    internal static async Task InitializeData(VotingAppContext dbContext)
    {
        Voter[] voters =
        [
            new("Christian Castle"),
            new("Luna Conflux", true),
        ];

        dbContext.Voters.AddRange(voters);

        Candidate[] candidates =
        [
            new("Drakon Fortress"),
            new("Josephine Tower", 2),
        ];

        dbContext.Candidates.AddRange(candidates);

        await dbContext.SaveChangesAsync();
    }
}
