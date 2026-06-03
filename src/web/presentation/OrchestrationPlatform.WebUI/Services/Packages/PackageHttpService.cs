using System.Net.Http.Headers;
using OrchestrationPlatform.WebUI.DTOs.Packages;
using OrchestrationPlatform.WebUI.Extensions;
using OrchestrationPlatform.WebUI.Models.Packages;

namespace OrchestrationPlatform.WebUI.Services.Packages;

public class PackageHttpService(HttpClient httpClient, ILogger<PackageHttpService> logger) : IPackageHttpService
{
    private const string BaseUrl = "api/packages";

    public async Task<List<PackageDto>> GetAllPackagesAsync()
    {
        try
        {
            return await httpClient.GetJsonAsync<List<PackageDto>>(BaseUrl) ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch all packages from API.");
            throw;
        }
    }

    public async Task CreatePackageAsync(CreatePackageFormModel model)
    {
        var response = await httpClient.PostJsonAsync(BaseUrl, model);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePackageAsync(Guid packageId)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/{packageId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<PackageVersionDto>> GetPackageVersionsAsync(Guid packageId)
    {
        try
        {
            return await httpClient.GetJsonAsync<List<PackageVersionDto>>($"{BaseUrl}/{packageId}/versions") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch package versions for package {PackageId}.", packageId);
            throw;
        }
    }

    public async Task UploadVersionAsync(Guid packageId, UploadArtifactFormModel model, Stream fileStream,
        string fileName, string contentType)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(model.Version), nameof(model.Version));
        content.Add(new StringContent(model.Architecture.ToString()!), nameof(model.Architecture));
        content.Add(new StringContent(model.OperatingSystemFamily.ToString()!), nameof(model.OperatingSystemFamily));
        content.Add(new StringContent(model.OperatingSystemVersion), nameof(model.OperatingSystemVersion));
        content.Add(new StringContent(model.PackageType.ToString()!), nameof(model.PackageType));

        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        content.Add(fileContent, "File", fileName);

        var response = await httpClient.PostAsync($"{BaseUrl}/{packageId}/versions", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteVersionAsync(Guid versionId)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/versions/{versionId}");
        response.EnsureSuccessStatusCode();
    }
}