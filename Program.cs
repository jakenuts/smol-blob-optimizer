```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace BlobOptimization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
            };

            try
            {
                var yamlReader = new YamlConfigurationReader();
                var config = yamlReader.ReadConfiguration("config.yaml");

                var blobServiceClient = new BlobServiceClient(config.AzureStorageAccountConnectionString);
                var blobStorageService = new BlobStorageService(blobServiceClient, config);
                var optimizationService = new OptimizationService(config);
                var progressIndicatorService = new ProgressIndicatorService();

                await foreach (var blob in blobStorageService.GetBlobsAsync(cancellationTokenSource.Token))
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation cancelled. Restarting...");
                        cancellationTokenSource = new CancellationTokenSource();
                        continue;
                    }

                    progressIndicatorService.ShowProgress(blob.Name);

                    var localFilePath = await blobStorageService.DownloadBlobAsync(blob, cancellationTokenSource.Token);
                    var optimizedFilePath = await optimizationService.OptimizeAsync(localFilePath, cancellationTokenSource.Token);
                    await blobStorageService.UploadBlobAsync(blob, optimizedFilePath, cancellationTokenSource.Token);

                    progressIndicatorService.ShowProgress(blob.Name, true);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
```