using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Application.Features.Operations.Commands.TriggerInstall;

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

    [HttpPost("trigger-install")]
    public async Task<IActionResult> TriggerInstall([FromBody] TriggerInstallCommand command,
        CancellationToken cancellationToken)
    {
        var operationId = await _mediator.Send(command, cancellationToken);

        return Accepted(new
        {
            OperationId = operationId,
            Message = "Installation workflow has been triggered successfully."
        });
    }
}