using OrchestrationPlatform.WebUI.DTOs.Packages;
using OrchestrationPlatform.WebUI.Models.Packages;

namespace OrchestrationPlatform.WebUI.Services.Packages;

public interface IPackageHttpService
{
    Task<List<PackageDto>> GetAllPackagesAsync();
    Task CreatePackageAsync(CreatePackageFormModel model);
    Task DeletePackageAsync(Guid packageId);
    Task<List<PackageVersionDto>> GetPackageVersionsAsync(Guid packageId);

    Task UploadVersionAsync(Guid packageId, UploadArtifactFormModel model, Stream fileStream, string fileName,
        string contentType);

    Task DeleteVersionAsync(Guid versionId);
}