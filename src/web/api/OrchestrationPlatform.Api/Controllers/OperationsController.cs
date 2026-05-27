using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Application.Features.Operations.Commands.TriggerInstall;
using OrchestrationPlatform.Application.Features.Operations.Commands.UpdateProgress;
using OrchestrationPlatform.Application.Features.Operations.Queries.GetHistory;

namespace OrchestrationPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> TriggerInstall([FromBody] TriggerInstallCommand command,
        CancellationToken cancellationToken)
    {
        var operationIds = await _mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Installation started", OperationIds = operationIds });
    }

    [HttpPost("{operationId}/callback")]
    public async Task<IActionResult> UpdateProgress(Guid operationId, [FromBody] UpdateOperationProgressCommand command,
        CancellationToken cancellationToken)
    {
        var actualCommand = command with { OperationId = operationId };

        await _mediator.Send(actualCommand, cancellationToken);

        return Ok(new { Message = "Progress updated successfully." });
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHostOperationHistory(
        [FromQuery] Guid hostId,
        [FromQuery] Guid packageVersionId,
        CancellationToken cancellationToken)
    {
        var query = new GetHostOperationHistoryQuery(hostId, packageVersionId);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}