using Microsoft.EntityFrameworkCore;
using VotingApp.Domain.Candidates;
using VotingApp.Domain.Context;
using VotingApp.Domain.Repositories;

namespace VotingApp.Application.Features.Candidates;
public class CandidatesRepository : RepositoryBase, ICandidatesRepository
{
    private readonly DbSet<Candidate> _candidates;
    public CandidatesRepository(VotingAppContext context)
        : base(context)
    {
        _candidates = _context.Set<Candidate>();
    }

    public async Task<Candidate[]> GetCandidates()
    {
        var result = await _candidates
            .ToArrayAsync();

        return result;
    }

    public async Task<Candidate> GetCandidateBy(int candidateId)
    {
        var result = await _candidates
            .Where(v => v.Id == candidateId)
            .FirstOrDefaultAsync();

        return result;
    }
}
