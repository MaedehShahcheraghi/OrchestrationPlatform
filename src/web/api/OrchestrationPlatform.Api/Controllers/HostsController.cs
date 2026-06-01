using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Application.Features.Hosts.Commands.CreateHost;
using OrchestrationPlatform.Application.Features.Hosts.Commands.DeleteHost;
using OrchestrationPlatform.Application.Features.Hosts.Commands.UpdateHost;
using OrchestrationPlatform.Application.Features.Hosts.Queries.GetAllHostsQuery;
using OrchestrationPlatform.Application.Features.Hosts.Queries.GetHost;

namespace OrchestrationPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHostCommand command, CancellationToken cancellationToken)
    {
        var hostId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = hostId }, hostId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var hosts = await mediator.Send(new GetAllHostsQuery(), cancellationToken);
        return Ok(hosts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHostById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetHostByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHostCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("ID in URL does not match ID in command.");

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteHostCommand(id), cancellationToken);
        return NoContent();
    }
}