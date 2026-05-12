using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestrationPlatform.Application.Abstractions.Models.ServiceModels
{
    public record UploadResult(
        string BucketName,
        string ObjectKey,
        long FileSize,
        string Sha256Hash
    );
}
