using OrchestrationPlatform.WebUI.Models.Hosts;

namespace OrchestrationPlatform.WebUI.Services.Hosts;

public class HostHttpService(HttpClient httpClient) : IHostHttpService
{
    public async Task<List<HostModel>> GetAllHostsAsync()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<List<HostModel>>("api/hosts") ?? new List<HostModel>();
        }
        catch
        {
            return new List<HostModel>();
        }
    }
}