using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Api.DTOs.Packages;
using OrchestrationPlatform.Application.Features.Packages.Commands.CreatePackage;
using OrchestrationPlatform.Application.Features.Packages.Commands.DeletePackage;
using OrchestrationPlatform.Application.Features.Packages.Commands.DeleteVersion;
using OrchestrationPlatform.Application.Features.Packages.Commands.UpdatePackage;
using OrchestrationPlatform.Application.Features.Packages.Commands.UpdateVersion;
using OrchestrationPlatform.Application.Features.Packages.Commands.UploadPackageVersion;
using OrchestrationPlatform.Application.Features.Packages.Queries.GetAllPackages;
using OrchestrationPlatform.Application.Features.Packages.Queries.GetPackageVersionsForSelect;

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
        var result = await mediator.Send(new GetPackageVersionsForSelectQuery(packageId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{packageId:guid}/versions")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadVersion(Guid packageId, [FromForm] UploadPackageVersionDto request,
        CancellationToken cancellationToken)
    {
        var command = new UploadPackageVersionCommand(
            packageId,
            request.Version,
            request.PackageType,
            request.OperatingSystemFamily,
            request.OperatingSystemVersion,
            request.Architecture,
            request.File.OpenReadStream(),
            request.File.FileName,
            request.File.ContentType,
            request.File.Length);

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

    [HttpDelete("versions/{versionId:guid}")]
    public async Task<IActionResult> DeleteVersion(Guid versionId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeletePackageVersionCommand(versionId), cancellationToken);
        return NoContent();
    }
}