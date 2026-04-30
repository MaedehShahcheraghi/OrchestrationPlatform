namespace OrchestrationPlatform.Domain.Enums;

public enum PackageType
{
    Deb = 1,
    Rpm = 2,
    TarGz = 3,
    Zip = 4,
    ShellScript = 5,
    Other = 99
}