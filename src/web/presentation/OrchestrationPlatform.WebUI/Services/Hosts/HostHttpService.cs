using OrchestrationPlatform.WebUI.DTOs.Hosts;
using OrchestrationPlatform.WebUI.Extensions;
using OrchestrationPlatform.WebUI.Models.Hosts;

namespace OrchestrationPlatform.WebUI.Services.Hosts;

public class HostHttpService(HttpClient httpClient) : IHostHttpService
{
    public async Task CreateHostAsync(CreateHostFormModel model)
    {
        var response = await httpClient.PostJsonAsync("api/hosts", model);
        response.EnsureSuccessStatusCode();
    }

    public async Task<HostDetailsDto?> GetHostByIdAsync(Guid id)
    {
        try
        {
            return await httpClient.GetJsonAsync<HostDetailsDto>($"api/hosts/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching host: {ex.Message}");
            return null;
        }
    }

    public async Task UpdateHostAsync(UpdateHostFormModel model)
    {
        var response = await httpClient.PutJsonAsync($"api/hosts/{model.Id}", model);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<HostDto>> GetAllHostsAsync()
    {
        try
        {
            return await httpClient.GetJsonAsync<List<HostDto>>("api/hosts") ?? new List<HostDto>();
        }
        catch
        {
            return new List<HostDto>();
        }
    }
}