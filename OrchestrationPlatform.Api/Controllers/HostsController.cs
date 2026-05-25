using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Application.Features.Hosts.Commands.CreateHost;
using OrchestrationPlatform.Application.Features.Hosts.Commands.DeleteHost;
using OrchestrationPlatform.Application.Features.Hosts.Commands.UpdateHost;
using OrchestrationPlatform.Application.Features.Hosts.Queries.GetAllHostsQuery;

namespace OrchestrationPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public HostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHostCommand command, CancellationToken cancellationToken)
    {
        var hostId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = hostId }, hostId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var hosts = await _mediator.Send(new GetAllHostsQuery(), cancellationToken);
        return Ok(hosts);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHostCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("ID in URL does not match ID in command.");

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteHostCommand(id), cancellationToken);
        return NoContent();
    }
}