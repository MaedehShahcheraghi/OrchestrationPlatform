using System.ComponentModel.DataAnnotations;
using OrchestrationPlatform.WebUI.Enums;

namespace OrchestrationPlatform.WebUI.Models.Hosts;

public class CreateHostFormModel
{
    [Required(ErrorMessage = "Host Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "IP Address is required")]
    [RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$", ErrorMessage = "Invalid IP Address format")]
    public string IpAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "SSH Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "SSH Port is required")]
    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535")]
    public int SshPort { get; set; } = 22;

    [Required(ErrorMessage = "OS Family is required")]
    public OperatingSystemFamily? OperatingSystemFamily { get; set; }

    [Required(ErrorMessage = "OS Version is required")]
    public string OperatingSystemVersion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Architecture is required")]
    public CpuArchitecture? Architecture { get; set; }
}