using VotingApp.Domain.Context;
using VotingApp.Domain.Repositories;

namespace VotingApp.Application.Features;
public class RepositoryBase(VotingAppContext context) : IRepositoryBase
{
    public readonly VotingAppContext _context = context;

    public async Task Add<TEntity>(TEntity entity) where TEntity : class
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task Save(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
