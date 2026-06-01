namespace OrchestrationPlatform.WebUI.DTOs.Hosts;

public record HostDto(
    Guid Id,
    string Name,
    string IpAddress,
    string Username,
    bool IsActive,
    DateTime CreatedAt);