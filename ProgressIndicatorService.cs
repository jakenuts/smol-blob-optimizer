```csharp
using System;
using System.Threading;

namespace BlobOptimization
{
    public class ProgressIndicatorService
    {
        private CancellationTokenSource _cancellationTokenSource;

        public ProgressIndicatorService()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void DisplayProgress(int current, int total)
        {
            Console.WriteLine($"Progress: {current}/{total} blobs processed.");
        }

        public void DisplayOptimizationProgress(string blobName)
        {
            Console.WriteLine($"Optimizing blob: {blobName}");
        }

        public void DisplayUploadProgress(string blobName)
        {
            Console.WriteLine($"Uploading optimized blob: {blobName}");
        }

        public CancellationToken GetCancellationToken()
        {
            return _cancellationTokenSource.Token;
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Restart()
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}
```