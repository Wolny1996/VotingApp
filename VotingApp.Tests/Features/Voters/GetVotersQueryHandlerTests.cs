using Microsoft.EntityFrameworkCore;
using VotingApp.Application.Features.Voters;
using VotingApp.Application.Features.Voters.Queries.GetVoters;
using VotingApp.Domain.Context;
using VotingApp.Tests.Features;
using VotingApp.Tests.Features.Voters;

namespace VotingAppTests.Features.Voters;
[TestFixture]
internal class GetVotersQueryHandlerTests
{
    private VotingAppContext _dbContext;

    private GetVotersQueryHandler _handler;
    private VotersRepository _votersRepository;

    [SetUp]
    public async Task Setup()
    {
        _dbContext = new(
            new DbContextOptionsBuilder<VotingAppContext>()
                .UseInMemoryDatabase(databaseName: $"TestingVotingAppContext")
                .Options);

        _dbContext.Database.EnsureDeleted();

        await VotersDatabaseData.InitializeData(_dbContext);

        _votersRepository = new VotersRepository(_dbContext);
        _handler = new GetVotersQueryHandler(_votersRepository);
    }

    [Test]
    public async Task GetVotersQueryHandler_Should_Return_AllVoters()
    {
        // Arrange
        var numberOfVoters = await _dbContext.Voters.CountAsync();
        var command = new GetVotersQuery();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckSuccessResult(result);
        result.Value.Voters.Should().HaveCountGreaterThan(0);
        result.Value.Voters.Should().HaveCount(numberOfVoters);
    }
}
