using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Emit;
using VotingApp.Domain.Candidates;
using VotingApp.Domain.Voters;

namespace VotingApp.Domain.Context;
public class VotingAppContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Voter> Voters { get; set; }

    public VotingAppContext(DbContextOptions<VotingAppContext> options) : base(options)
    {
        Candidate[] c = [
            new Candidate("C1"),
            new Candidate("C2")
        ];

        //modelBuilder.Entity<Voter>().HasData(
        //    new Voter("V1"),
        //    new Voter("V2")
        //);

        Candidates.Add(c[0]);
        Candidates.Add(c[1]);
        var ch = Candidates.Local.ToList<Candidate>(); ;
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Seed();
    //}
}