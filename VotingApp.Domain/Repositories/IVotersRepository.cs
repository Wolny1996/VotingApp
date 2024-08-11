using VotingApp.Domain.Voters;

namespace VotingApp.Domain.Repositories;
public interface IVotersRepository : IRepositoryBase
{
    Task<Voter[]> GetVoters();

    Task<Voter> GetVoterBy(int voterId);
}
