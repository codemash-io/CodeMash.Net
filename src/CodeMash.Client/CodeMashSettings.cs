using System;
using System.IO;
using CodeMash.Interfaces.Client;
using Microsoft.Extensions.Configuration;

namespace CodeMash.Client
{
    public class CodeMashSettings : ICodeMashSettings
    {
        public string ApiBaseUrl { get; }
        
        public string CultureCode { get; set; }

        public string Version { get; set; } = "v2";

        public string SecretKey { get; }

        public Guid ProjectId { get; }
        
        public CodeMashSettings(IConfigurationRoot config = null, string settingsFileName = "appsettings.json")
        {
            config ??= new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(settingsFileName)
                .Build();
            
            SecretKey = config["CodeMash:ApiKey"];
            ApiBaseUrl = config["CodeMash:ApiBaseUri"] ?? "https://api.codemash.io/";

            var projectIdParsed = Guid.TryParse(config["CodeMash:ProjectId"], out var parsedProjectId);
            if (projectIdParsed) ProjectId = parsedProjectId;
            else
            {
                throw new ArgumentNullException(nameof(ProjectId), "Project ID is not of type Guid");
            }
        }
        
        public CodeMashSettings(string apiBaseUrl, string apiKey, Guid projectId)
        {
            ApiBaseUrl = apiBaseUrl ?? "https://api.codemash.io/";
            SecretKey = apiKey;
            ProjectId = projectId;
        }
    }
}
