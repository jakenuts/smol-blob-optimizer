```csharp
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace BlobOptimization
{
    public class OptimizationService
    {
        private readonly Configuration _config;
        private readonly BlobStorageService _blobStorageService;

        public OptimizationService(Configuration config, BlobStorageService blobStorageService)
        {
            _config = config;
            _blobStorageService = blobStorageService;
        }

        public async Task OptimizeBlobAsync(BlobItem blob)
        {
            var localFilePath = await _blobStorageService.DownloadBlobAsync(blob);

            try
            {
                await RunOptimizationProgramAsync(localFilePath);
                await _blobStorageService.UploadBlobAsync(localFilePath, blob.Name);
            }
            finally
            {
                File.Delete(localFilePath);
            }
        }

        private async Task RunOptimizationProgramAsync(string filePath)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = _config.OptimizationProgramPath,
                Arguments = $"{_config.OptimizationProgramArguments} {filePath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);

            if (process == null)
            {
                throw new Exception("Failed to start optimization program.");
            }

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                var errorOutput = await process.StandardError.ReadToEndAsync();
                throw new Exception($"Optimization program failed with exit code {process.ExitCode}: {errorOutput}");
            }
        }
    }
}
```