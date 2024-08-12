using MediatR;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Application.Features.Candidates.Commands.AddCandidate;
using VotingApp.Application.Features.Candidates.Queries.GetCandidates;

namespace VotingApp.Controllers;
[ApiController]
[Route("[controller]")]
public class CandidatesController(
    ISender sender) : ApiController(sender)
{
    [HttpGet]
    [Route("get-candidates")]
    public async Task<IActionResult> GetCandidates()
    {
        var query = new GetCandidatesQuery();

        var result = await _sender.Send(query);
        return result.IsSuccess
            ? Ok(result.Value.Candidates)
            : HandleFailure(result.Error);
    }

    [HttpPost]
    [Route("add-candidate")]
    public async Task<IActionResult> AddCandidate(AddCandidateRequest request)
    {
        var command = new AddCandidateCommand(request.FullName);

        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : HandleFailure(result.Error);
    }
}
