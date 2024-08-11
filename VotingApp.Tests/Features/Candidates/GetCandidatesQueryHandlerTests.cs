using Microsoft.EntityFrameworkCore;
using VotingApp.Application.Features.Candidates;
using VotingApp.Application.Features.Candidates.Queries.GetCandidates;
using VotingApp.Domain.Context;
using VotingApp.Tests.Features;
using VotingApp.Tests.Features.Candidates;

namespace VotingAppTests.Features.Candidates;

[TestFixture]
internal class GetCandidatesQueryHandlerTests
{
    private VotingAppContext _dbContext;

    private GetCandidatesQueryHandler _handler;
    private CandidatesRepository _candidatesRepository;

    [SetUp]
    public async Task Setup()
    {
        _dbContext = new(
            new DbContextOptionsBuilder<VotingAppContext>()
                .UseInMemoryDatabase(databaseName: $"TestingVotingAppContext")
                .Options);

        _dbContext.Database.EnsureDeleted();

        await CandidatesDatabaseData.InitializeData(_dbContext);

        _candidatesRepository = new CandidatesRepository(_dbContext);
        _handler = new GetCandidatesQueryHandler(_candidatesRepository);
    }

    [Test]
    public async Task GetCandidatesQueryHandler_Should_Return_AllCandidates()
    {
        // Arrange
        var numberOfCandidates = await _dbContext.Candidates.CountAsync();
        var command = new GetCandidatesQuery();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckSuccessResult(result);
        result.Value.Candidates.Should().HaveCountGreaterThan(0);
        result.Value.Candidates.Should().HaveCount(numberOfCandidates);
    }
}
