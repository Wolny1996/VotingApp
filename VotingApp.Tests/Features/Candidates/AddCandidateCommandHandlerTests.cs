using Microsoft.EntityFrameworkCore;
using VotingApp.Application.Features.Candidates;
using VotingApp.Application.Features.Candidates.Commands.AddCandidate;
using VotingApp.Domain.Context;
using VotingApp.Tests.Features;
using VotingApp.Tests.Features.Candidates;

namespace VotingAppTests.Features.Candidates;

[TestFixture]
internal class AddCandidateCommandHandlerTests
{
    private VotingAppContext _dbContext;

    private AddCandidateCommandHandler _handler;
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
        _handler = new AddCandidateCommandHandler(_candidatesRepository);
    }

    [Test]
    public async Task AddCandidateCommandHandler_Should_Add_NewCandidate()
    {
        // Arrange
        var initialNumberOfCandidates = await _dbContext.Candidates.CountAsync();
        var command = new AddCandidateCommand("Sandro Necropolis");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckSuccessResult(result);

        result.Value.Should().NotBe(default);
        _dbContext.Candidates.Should().HaveCount(initialNumberOfCandidates + 1);
    }
}
