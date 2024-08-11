namespace VotingApp.Domain.Repositories;
public interface IRepositoryBase
{
    Task Add<TEntity>(TEntity entity) where TEntity : class;

    Task Save(CancellationToken cancellationToken = default);
}
