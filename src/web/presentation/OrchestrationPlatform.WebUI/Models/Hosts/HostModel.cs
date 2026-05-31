namespace OrchestrationPlatform.WebUI.Models.Hosts;

public record HostModel(
    Guid Id,
    string Name,
    string IpAddress,
    string Username,
    bool IsActive,
    DateTime CreatedAt);