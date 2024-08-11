using Microsoft.EntityFrameworkCore;
using VotingApp.Domain.Context;
using VotingApp.Domain.Repositories;
using VotingApp.Domain.Voters;

namespace VotingApp.Application.Features.Voters;
public class VotersRepository : RepositoryBase, IVotersRepository
{
    private readonly DbSet<Voter> _voters;
    public VotersRepository(VotingAppContext context)
        : base(context)
    {
        _voters = _context.Set<Voter>();
    }

    public async Task<Voter[]> GetVoters()
    {
        var result = await _voters
            .ToArrayAsync();

        return result;
    }

    public async Task<Voter> GetVoterBy(int voterId)
    {
        var result = await _voters
            .Where(v => v.Id == voterId)
            .FirstOrDefaultAsync();

        return result;
    }
}
