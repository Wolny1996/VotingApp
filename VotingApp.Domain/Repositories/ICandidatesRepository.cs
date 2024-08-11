using VotingApp.Domain.Candidates;

namespace VotingApp.Domain.Repositories;
public interface ICandidatesRepository : IRepositoryBase
{
    Task<Candidate[]> GetCandidates();

    Task<Candidate> GetCandidateBy(int candidateId);
}
