using Microsoft.EntityFrameworkCore;
using VotingApp.Domain.Candidates;
using VotingApp.Domain.Voters;

namespace VotingApp.Domain.Context;
public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>().HasData(
            new Candidate("C1"),
            new Candidate("C2")
        );

        modelBuilder.Entity<Voter>().HasData(
            new Voter("V1"),
            new Voter("V2")
        );
    }
}
