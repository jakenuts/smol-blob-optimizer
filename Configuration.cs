```csharp
using System.Collections.Generic;

public class Configuration
{
    public string AzureStorageAccountConnectionString { get; set; }
    public string ContainerName { get; set; }
    public string SearchPath { get; set; }
    public List<string> FileExtensions { get; set; }
    public double MinimumSizeMB { get; set; }
    public string OptimizationProgramPath { get; set; }
    public string OptimizationProgramArguments { get; set; }
}
```