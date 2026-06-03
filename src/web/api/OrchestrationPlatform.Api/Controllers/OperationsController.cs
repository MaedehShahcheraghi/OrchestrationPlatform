using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Application.Features.Operations.Commands.TriggerInstall;
using OrchestrationPlatform.Application.Features.Operations.Commands.TriggerUninstall;
using OrchestrationPlatform.Application.Features.Operations.Commands.UpdateProgress;
using OrchestrationPlatform.Application.Features.Operations.Queries.GetHistory;
using OrchestrationPlatform.Application.Features.Operations.Queries.GetOperationLogs;

namespace OrchestrationPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationsController(IMediator mediator) : ControllerBase
{
    [HttpPost("install")]
    public async Task<IActionResult> TriggerInstall([FromBody] TriggerInstallCommand command,
        CancellationToken cancellationToken)
    {
        var operationMap = await mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Installation started", OperationHostMapping = operationMap });
    }

    [HttpPost("uninstall")]
    public async Task<IActionResult> TriggerUnInstall([FromBody] TriggerUninstallCommand command,
        CancellationToken cancellationToken)
    {
        var operationMap = await mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Installation started", OperationHostMapping = operationMap });
    }

    [HttpPost("{operationId}/callback")]
    public async Task<IActionResult> UpdateProgress(Guid operationId, [FromBody] UpdateOperationProgressCommand command,
        CancellationToken cancellationToken)
    {
        var actualCommand = command with { OperationId = operationId };

        await mediator.Send(actualCommand, cancellationToken);

        return Ok(new { Message = "Progress updated successfully." });
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHostOperationHistory(
        [FromQuery] GetHostOperationHistoryQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{operationId:guid}/logs")]
    public async Task<IActionResult> GetOperationLogs(Guid operationId, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetOperationLogsQuery(operationId), cancellationToken));
    }
}