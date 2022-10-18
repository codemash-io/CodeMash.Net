using Microsoft.Extensions.Configuration;

namespace CodeMash.Tests;

public class CodeMashConfiguration
{
    public IConfigurationRoot Settings { get; private set; }

    public CodeMashConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        configurationBuilder.AddJsonFile(path, false);

        Settings = configurationBuilder.Build();
    }
    
    public string ApiBaseUri => $"{Settings.GetSection("CodeMash").GetSection("ApiBaseUri").Value}";
    public string HubBaseUri => $"{Settings.GetSection("CodeMash").GetSection("Hub").GetSection("ApiBaseUri").Value}";
    public const string V2 = "v2";
    
    public string AccountId => Settings.GetSection("CodeMash").GetSection("AccountId").Value;
    public string ProjectId => Settings.GetSection("CodeMash").GetSection("ProjectId").Value;
    public string ApiKey => Settings.GetSection("CodeMash").GetSection("ApiKey").Value;
}