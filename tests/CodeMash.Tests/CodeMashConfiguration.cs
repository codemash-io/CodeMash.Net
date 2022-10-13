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
    
    public string ApiBaseUri => $"{Settings.GetSection("Api").GetSection("BaseUri").Value}";
    public string HubBaseUri => $"{Settings.GetSection("Hub").GetSection("BaseUri").Value}";
    public const string V2 = "v2";
    
    public string AccountId => Settings.GetSection("Context").GetSection("AccountId").Value;
    public string ProjectId => Settings.GetSection("Context").GetSection("ProjectId").Value;
    public string SysAdminToken => Settings.GetSection("Context").GetSection("SysAdminToken").Value;
    public string ApiAdminToken => Settings.GetSection("Api").GetSection("AdminToken").Value;
}