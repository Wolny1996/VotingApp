using Microsoft.EntityFrameworkCore;
using VotingApp.Application.Features.Voters;
using VotingApp.Application.Features.Voters.Commands.AddVoter;
using VotingApp.Domain.Context;
using VotingApp.Tests.Features;
using VotingApp.Tests.Features.Voters;

namespace VotingAppTests.Features.Voters;

[TestFixture]
internal class AddVoterCommandHandlerTests
{
    private VotingAppContext _dbContext;

    private AddVoterCommandHandler _handler;
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
        _handler = new AddVoterCommandHandler(_votersRepository);
    }

    [Test]
    public async Task AddVoterCommandHandler_Should_Add_NewVoter()
    {
        // Arrange
        var initialNumberOfVoters = await _dbContext.Voters.CountAsync();
        var command = new AddVoterCommand("Shakti Dungeon");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckSuccessResult(result);

        result.Value.Should().NotBe(default);
        _dbContext.Voters.Should().HaveCount(initialNumberOfVoters + 1);
    }
}
