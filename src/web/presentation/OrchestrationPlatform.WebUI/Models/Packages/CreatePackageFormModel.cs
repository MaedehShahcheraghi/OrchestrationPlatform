using System.ComponentModel.DataAnnotations;

namespace OrchestrationPlatform.WebUI.Models.Packages;

public class CreatePackageFormModel
{
    [Required] public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}