using OrchestrationPlatform.WebUI.DTOs.Operations;
using OrchestrationPlatform.WebUI.Extensions;
using OrchestrationPlatform.WebUI.Models.Common;

namespace OrchestrationPlatform.WebUI.Services.Operations;

public class OperationHttpService(HttpClient httpClient, ILogger<OperationHttpService> logger) : IOperationHttpService
{
    private const string BaseUrl = "api/operations";

    public async Task<Dictionary<Guid, Guid>> TriggerInstallAsync(InstallOperationDto request)
    {
        var response = await httpClient.PostJsonAsync($"{BaseUrl}/install", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TriggerOperationResponseDto>();
        return result?.OperationHostMapping ?? new Dictionary<Guid, Guid>();
    }

    public async Task<Dictionary<Guid, Guid>> TriggerUninstallAsync(InstallOperationDto request)
    {
        var response = await httpClient.PostJsonAsync($"{BaseUrl}/uninstall", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TriggerOperationResponseDto>();
        return result?.OperationHostMapping ?? new Dictionary<Guid, Guid>();
    }

    public async Task<PagedResult<OperationHistoryDto>> GetHostHistoryAsync(Guid hostId, int pageNumber = 1,
        int pageSize = 10)
    {
        try
        {
            return await httpClient.GetJsonAsync<PagedResult<OperationHistoryDto>>(
                       $"{BaseUrl}/history?hostId={hostId}&pageNumber={pageNumber}&pageSize={pageSize}") ??
                   new PagedResult<OperationHistoryDto>();
        }
        catch
        {
            return new PagedResult<OperationHistoryDto>();
        }
    }

    public async Task<List<OperationLogDto>> GetOperationLogsAsync(Guid operationId)
    {
        return await httpClient.GetJsonAsync<List<OperationLogDto>>($"{BaseUrl}/{operationId}/logs") ??
               new List<OperationLogDto>();
    }

    public async Task<Dictionary<Guid, Guid>> TriggerConfigureAsync(ConfigureOperationDto request)
    {
        var response = await httpClient.PostJsonAsync($"{BaseUrl}/configure", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TriggerOperationResponseDto>();
        return result?.OperationHostMapping ?? new Dictionary<Guid, Guid>();
    }
}