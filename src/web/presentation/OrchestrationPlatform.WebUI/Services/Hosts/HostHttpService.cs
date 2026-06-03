using System.Net;
using OrchestrationPlatform.WebUI.DTOs.Hosts;
using OrchestrationPlatform.WebUI.DTOs.Operations;
using OrchestrationPlatform.WebUI.Extensions;
using OrchestrationPlatform.WebUI.Models.Hosts;

namespace OrchestrationPlatform.WebUI.Services.Hosts;

public class HostHttpService(HttpClient httpClient, ILogger<HostHttpService> logger) : IHostHttpService
{
    private const string BaseUrl = "api/hosts";

    public async Task CreateHostAsync(CreateHostFormModel model)
    {
        var response = await httpClient.PostJsonAsync(BaseUrl, model);
        response.EnsureSuccessStatusCode();
    }

    public async Task<HostDetailsDto?> GetHostByIdAsync(Guid id)
    {
        try
        {
            return await httpClient.GetJsonAsync<HostDetailsDto>($"{BaseUrl}/{id}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            logger.LogWarning("Host with ID {HostId} was not found.", id);
            return null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching host with ID {HostId}.", id);
            throw;
        }
    }

    public async Task DeleteHostAsync(Guid id)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateHostAsync(UpdateHostFormModel model)
    {
        var response = await httpClient.PutJsonAsync($"{BaseUrl}/{model.Id}", model);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<HostDto>> GetAllHostsAsync()
    {
        try
        {
            return await httpClient.GetJsonAsync<List<HostDto>>(BaseUrl) ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to fetch all hosts from API.");
            throw;
        }
    }

    public async Task<List<InstalledSoftwareDto>> GetInstalledSoftwaresAsync(Guid hostId)
    {
        return await httpClient.GetJsonAsync<List<InstalledSoftwareDto>>($"{BaseUrl}/{hostId}/installed-softwares") ??
               new List<InstalledSoftwareDto>();
    }
}