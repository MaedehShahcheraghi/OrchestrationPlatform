using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Application.Features.Packages.Commands.CreatePackage;
using OrchestrationPlatform.Application.Features.Packages.Commands.DeletePackage;
using OrchestrationPlatform.Application.Features.Packages.Commands.UpdatePackage;
using OrchestrationPlatform.Application.Features.Packages.Queries.GetAllPackages;
using OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Commands.CreateVersion;
using OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Commands.UpdateVersion;
using OrchestrationPlatform.Application.Features.SoftwarePackageVersion.Queries.GetVersionsByPackageId;

namespace OrchestrationPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPackages(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllPackagesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePackage([FromBody] CreatePackageCommand command,
        CancellationToken cancellationToken)
    {
        var packageId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAllPackages), new { id = packageId }, new { Id = packageId });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePackage(Guid id, [FromBody] UpdatePackageCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return BadRequest("ID mismatch");
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePackage(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeletePackageCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{packageId:guid}/versions")]
    public async Task<IActionResult> GetPackageVersions(Guid packageId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetVersionsByPackageIdQuery(packageId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{packageId:guid}/versions")]
    public async Task<IActionResult> CreateVersion(Guid packageId, [FromBody] CreatePackageVersionCommand command,
        CancellationToken cancellationToken)
    {
        if (packageId != command.SoftwarePackageId) return BadRequest("Package ID mismatch");
        var versionId = await mediator.Send(command, cancellationToken);
        return Ok(new { Id = versionId });
    }

    [HttpPut("versions/{versionId:guid}")]
    public async Task<IActionResult> UpdateVersion(Guid versionId, [FromBody] UpdatePackageVersionCommand command,
        CancellationToken cancellationToken)
    {
        if (versionId != command.Id) return BadRequest("Version ID mismatch");
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}