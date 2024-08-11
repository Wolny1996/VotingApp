using Microsoft.EntityFrameworkCore;
using VotingApp.Application.Features.Candidates;
using VotingApp.Application.Features.Voters;
using VotingApp.Application.Features.Voters.Commands.VoterVoted;
using VotingApp.Domain.Candidates;
using VotingApp.Domain.Context;
using VotingApp.Domain.Voters;
using VotingApp.Tests.Features;
using VotingApp.Tests.Features.Voters;

namespace VotingAppTests.Features.Voters;

[TestFixture]
internal class VoterVotedCommandHandlerTests
{
    private VotingAppContext _dbContext;

    private VoterVotedCommandHandler _handler;
    
    private CandidatesRepository _candidatesRepository;
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

        _candidatesRepository = new CandidatesRepository(_dbContext);
        _votersRepository = new VotersRepository(_dbContext);

        _handler = new VoterVotedCommandHandler(
            _candidatesRepository,
            _votersRepository);
    }

    [Test]
    public async Task VoterVotedCommandHandler_Should_Change_VoterHasVotedFlagToTrue_And_AddVoteToGivenCandidate()
    {
        // Arrange
        var voter = await _dbContext.Voters.FirstAsync();
        var initialStateOfVoterHasVotedFlag = voter.HasVoted;

        var candidate = await _dbContext.Candidates.FirstAsync();
        var initialNumberOfCandiidateVotes = candidate.NumberOfVotes;

        var command = new VoterVotedCommand(candidate.Id, voter.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckSuccessResult(result);

        var updatedVoter = await _dbContext.Voters
            .Where(v => v.Id == voter.Id)
            .FirstAsync();

        updatedVoter.HasVoted.Should().BeTrue();
        updatedVoter.HasVoted.Should().Be(!initialStateOfVoterHasVotedFlag);

        var updatedCandidate = await _dbContext.Candidates
            .Where(c => c.Id == candidate.Id)
            .FirstAsync();

        updatedCandidate.NumberOfVotes.Should().Be(initialNumberOfCandiidateVotes + 1);
    }

    [Test]
    public async Task VoterVotedCommandHandler_Should_Return_NotFoundError_WhenVoterDoesNotExists()
    {
        // Arrange
        var candidate = await _dbContext.Candidates.FirstAsync();

        var command = new VoterVotedCommand(candidate.Id, 0);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckFailureResult(result, VotersErrors.NotFound);
    }

    [Test]
    public async Task VoterVotedCommandHandler_Should_Return_AlreadyCastVoteError_WhenVoterHasVotedFlagEqualsTrue()
    {
        // Arrange
        var voter = await _dbContext.Voters
            .Where(v => v.HasVoted)
            .FirstAsync();

        var candidate = await _dbContext.Candidates.FirstAsync();

        var command = new VoterVotedCommand(candidate.Id, voter.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckFailureResult(result, VotersErrors.AlreadyCastVote);
    }

    [Test]
    public async Task VoterVotedCommandHandler_Should_Return_NotFoundError_WhenCandidateDoesNotExists()
    {
        // Arrange
        var voter = await _dbContext.Voters.FirstAsync();

        var command = new VoterVotedCommand(0, voter.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ResultChecker.CheckFailureResult(result, CandidatesErrros.NotFound);
    }
}
