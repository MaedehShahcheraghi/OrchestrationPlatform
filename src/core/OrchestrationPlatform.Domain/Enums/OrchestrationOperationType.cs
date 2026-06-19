namespace OrchestrationPlatform.Domain.Enums;

public enum OrchestrationOperationType
{
    Install = 1,
    Uninstall = 2,
    Upgrade = 3,
    Reinstall = 4,
    ConfigureFirewall = 5,
    ConfigureDns = 6,
    ManageService = 7
}