using Microsoft.AspNetCore.SignalR;
using OrchestrationPlatform.Api.Hubs;
using OrchestrationPlatform.Application.Abstractions.Services.Api;

namespace OrchestrationPlatform.Api.Services;

public class SignalROperationNotifierService(IHubContext<OperationHub> hubContext) : IOperationNotifierService
{
    public async Task NotifyProgressAsync(
        Guid operationId,
        string status,
        int progressPercent,
        string message,
        CancellationToken cancellationToken = default)
    {
        await hubContext.Clients.Group(operationId.ToString()).SendAsync(
            "ReceiveOperationUpdate",
            new { OperationId = operationId, Status = status, Progress = progressPercent, Message = message },
            cancellationToken);
    }
}