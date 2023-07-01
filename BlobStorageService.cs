```csharp
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly Configuration _configuration;

    public BlobStorageService(Configuration configuration)
    {
        _configuration = configuration;
        _blobServiceClient = new BlobServiceClient(_configuration.AzureStorageAccountConnectionString);
    }

    public async Task<IEnumerable<BlobItem>> GetBlobsAsync()
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration.ContainerName);
        var blobs = new List<BlobItem>();
        await foreach (var blob in containerClient.GetBlobsAsync())
        {
            if (blob.Properties.ContentLength >= _configuration.MinimumSize * 1024 * 1024 &&
                _configuration.FileExtensions.Contains(Path.GetExtension(blob.Name)) &&
                !blob.Metadata.ContainsKey("Optimized"))
            {
                blobs.Add(blob);
            }
        }
        return blobs;
    }

    public async Task DownloadBlobAsync(BlobItem blobItem)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration.ContainerName);
        var blobClient = containerClient.GetBlobClient(blobItem.Name);
        await blobClient.DownloadToAsync($"{Path.GetTempPath()}{blobItem.Name}");
    }

    public async Task UploadBlobAsync(BlobItem blobItem)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration.ContainerName);
        var blobClient = containerClient.GetBlobClient(blobItem.Name);
        var blobUploadOptions = new BlobUploadOptions
        {
            Metadata = blobItem.Metadata,
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = blobItem.Properties.ContentType,
                ContentHash = blobItem.Properties.ContentHash,
                ContentEncoding = blobItem.Properties.ContentEncoding,
                ContentLanguage = blobItem.Properties.ContentLanguage,
                ContentDisposition = blobItem.Properties.ContentDisposition,
                CacheControl = blobItem.Properties.CacheControl
            }
        };
        blobUploadOptions.Metadata["Optimized"] = "true";
        await blobClient.UploadAsync($"{Path.GetTempPath()}{blobItem.Name}", blobUploadOptions);
    }
}
```