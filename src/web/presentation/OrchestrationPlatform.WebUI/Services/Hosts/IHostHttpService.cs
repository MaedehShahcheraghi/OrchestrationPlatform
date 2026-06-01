using OrchestrationPlatform.WebUI.DTOs.Hosts;
using OrchestrationPlatform.WebUI.Models.Hosts;

namespace OrchestrationPlatform.WebUI.Services.Hosts;

public interface IHostHttpService
{
    Task<List<HostDto>> GetAllHostsAsync();
    Task<HostDetailsDto?> GetHostByIdAsync(Guid id);
    Task UpdateHostAsync(UpdateHostFormModel model);
    Task CreateHostAsync(CreateHostFormModel model);
}