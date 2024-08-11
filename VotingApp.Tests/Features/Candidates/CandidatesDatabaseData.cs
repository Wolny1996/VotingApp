using VotingApp.Domain.Candidates;
using VotingApp.Domain.Context;

namespace VotingApp.Tests.Features.Candidates;
internal static class CandidatesDatabaseData
{
    internal static async Task InitializeData(VotingAppContext dbContext)
    {
        Candidate[] candidates =
        [
            new("Drakon Fortress"),
            new("Josephine Tower", 2),
        ];

        dbContext.Candidates.AddRange(candidates);

        await dbContext.SaveChangesAsync();
    }
}
