```csharp
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class YamlConfigurationReader
{
    public Configuration ReadConfiguration(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var configuration = deserializer.Deserialize<Configuration>(reader);
            return configuration;
        }
    }
}
```