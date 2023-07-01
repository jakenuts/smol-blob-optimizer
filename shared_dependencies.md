Shared Dependencies:

1. "Azure.Storage.Blobs" package: This package is used in "BlobStorageService.cs" for connecting to the Azure Storage Account and performing operations on blobs. It is also used in "Program.cs" to initiate the connection to the Azure Storage Account.

2. "Configuration" class: This class is defined in "Configuration.cs" and is used in "Program.cs" to hold the configuration data read from the yaml file. It is also used in "BlobStorageService.cs" to get the necessary parameters for blob operations and in "YamlConfigurationReader.cs" to map the yaml data.

3. "BlobStorageService" class: This class is defined in "BlobStorageService.cs" and is used in "Program.cs" to perform operations on blobs. It is also used in "OptimizationService.cs" to download and upload blobs.

4. "OptimizationService" class: This class is defined in "OptimizationService.cs" and is used in "Program.cs" to optimize blobs. It uses the "Configuration" class to get the path and arguments to the optimization program.

5. "ProgressIndicatorService" class: This class is defined in "ProgressIndicatorService.cs" and is used in "Program.cs" to display progress indicators. It may also be used in "BlobStorageService.cs" and "OptimizationService.cs" to update the progress.

6. "YamlConfigurationReader" class: This class is defined in "YamlConfigurationReader.cs" and is used in "Program.cs" to read the yaml configuration file. It uses the "Configuration" class to map the yaml data.

7. "App.config" file: This file is used in "Program.cs" to configure the application settings. It may also be used in other classes to get the application settings.

8. "Optimized" tag: This tag is used in "BlobStorageService.cs" to mark a blob as optimized. It is also used in "Program.cs" to filter out already optimized blobs.

9. "BatchSize" variable: This variable is used in "BlobStorageService.cs" to determine the number of blobs to process in each batch. It is also used in "Program.cs" to control the batch processing.

10. "Cancel" and "Restart" commands: These commands are used in "Program.cs" to control the execution of the application. They may also be used in "ProgressIndicatorService.cs" to update the progress based on the user's actions.