using System.ComponentModel.DataAnnotations;

namespace OrchestrationPlatform.WebUI.Models.Hosts;

public class UpdateHostFormModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Host Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "IP Address is required")]
    [RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$", ErrorMessage = "Invalid IP Address format")]
    public string IpAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "SSH Username is required")]
    public string Username { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}