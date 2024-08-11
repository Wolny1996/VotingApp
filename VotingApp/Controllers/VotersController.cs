using MediatR;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Application.Features.Voters.Commands.AddVoter;
using VotingApp.Application.Features.Voters.Commands.VoterVoted;
using VotingApp.Application.Features.Voters.Queries.GetVoters;

namespace VotingApp.Controllers;
[ApiController]
[Route("[controller]")]
public class VotersController(
    ISender sender) : ApiController(sender)
{
    [HttpGet]
    [Route("get-voters")]
    public async Task<IActionResult> GetVoters()
    {
        var query = new GetVotersQuery();

        var result = await _sender.Send(query);
        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result.Error);
    }

    [HttpPost]
    [Route("add-voter")]
    public async Task<IActionResult> AddVoter(AddVoterRequest request)
    {
        var command = new AddVoterCommand(request.FullName);

        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : HandleFailure(result.Error);
    }

    [HttpPut]
    [Route("voter-voted")]
    public async Task<IActionResult> VoterVoted(VoterVotedRequest request)
    {
        var command = new VoterVotedCommand(
            request.CandidateId,
            request.VoterId);

        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : HandleFailure(result.Error);
    }
}
