using OrchestrationPlatform.WebUI.DTOs.Operations;
using OrchestrationPlatform.WebUI.Models.Common;

namespace OrchestrationPlatform.WebUI.Services.Operations;

public interface IOperationHttpService
{
    Task<Dictionary<Guid, Guid>> TriggerInstallAsync(InstallOperationDto request);
    Task<Dictionary<Guid, Guid>> TriggerUninstallAsync(InstallOperationDto request);
    Task<PagedResult<OperationHistoryDto>> GetHostHistoryAsync(Guid hostId, int pageNumber = 1, int pageSize = 10);
    Task<List<OperationLogDto>> GetOperationLogsAsync(Guid operationId);

    Task<Dictionary<Guid, Guid>> TriggerConfigureAsync(ConfigureOperationDto request);
}