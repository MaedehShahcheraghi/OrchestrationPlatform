using OrchestrationPlatform.WebUI.Models.Hosts;

namespace OrchestrationPlatform.WebUI.Services.Hosts;

public interface IHostHttpService
{
    Task<List<HostModel>> GetAllHostsAsync();
}