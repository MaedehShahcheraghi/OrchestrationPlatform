using Microsoft.AspNetCore.SignalR;

namespace OrchestrationPlatform.Api.Hubs;

public class OperationHub : Hub
{
    public async Task JoinOperationGroup(string operationId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, operationId);
    }

    public async Task LeaveOperationGroup(string operationId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, operationId);
    }
}