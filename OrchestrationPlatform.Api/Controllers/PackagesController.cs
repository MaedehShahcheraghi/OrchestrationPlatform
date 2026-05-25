using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrchestrationPlatform.Application.Features.Packages.Commands.UploadPackage;
using OrchestrationPlatform.Application.Features.Packages.Queries.GetPackagesForSelect;
using OrchestrationPlatform.Application.Features.Packages.Queries.GetPackageVersionsForSelect;

namespace OrchestrationPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PackagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{versionId:guid}/upload")]
    public async Task<IActionResult> UploadPackageArtifact(Guid versionId, IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty.");

        // باز کردن استریم در لایه نمایش و پاس دادن آن به هسته مرکزی
        await using var stream = file.OpenReadStream();

        var command = new UploadPackageCommand(
            versionId,
            stream,
            file.FileName,
            file.ContentType,
            file.Length);

        var artifactId = await _mediator.Send(command, cancellationToken);

        return Ok(new { ArtifactId = artifactId, Message = "File uploaded successfully." });
    }

    [HttpGet("select-items")]
    public async Task<IActionResult> GetPackagesForSelect(CancellationToken cancellationToken)
    {
        var query = new GetPackagesForSelectQuery();
        var packages = await _mediator.Send(query, cancellationToken);

        return Ok(packages);
    }

    // گرفتن لیست ورژن‌ها برای سلکت‌باکس دوم (وابسته به انتخاب اول)
    [HttpGet("{packageId:guid}/versions/select-items")]
    public async Task<IActionResult> GetPackageVersionsForSelect(Guid packageId, CancellationToken cancellationToken)
    {
        var query = new GetPackageVersionsForSelectQuery(packageId);
        var versions = await _mediator.Send(query, cancellationToken);

        return Ok(versions);
    }
}