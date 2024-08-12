using Microsoft.EntityFrameworkCore;
using VotingApp.Domain.Candidates;
using VotingApp.Domain.Voters;

namespace VotingApp.Domain.Context;
public class VotingAppContext(DbContextOptions<VotingAppContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Voter> Voters { get; set; }
}