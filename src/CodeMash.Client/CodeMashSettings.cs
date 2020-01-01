using System;
using System.IO;
using CodeMash.Interfaces.Client;
using Microsoft.Extensions.Configuration;

namespace CodeMash.Client
{
    public class CodeMashSettings : ICodeMashSettings
    {
        public const string ApiUrl = "https://api.codemash.io/";
        
        public string CultureCode { get; set; }

        public string Version { get; set; } = "v1";

        public string SecretKey { get; }

        public Guid ProjectId { get; }
        
        public CodeMashSettings(IConfigurationRoot config, string settingsFileName = "appsettings.json")
        {
            if (config == null)
            {
                config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(settingsFileName).Build();
            }
            
            SecretKey = config["CodeMash:ApiKey"];

            var projectIdParsed = Guid.TryParse(config["CodeMash:ProjectId"], out var parsedProjectId);
            if (projectIdParsed) ProjectId = parsedProjectId;
            else
            {
                throw new ArgumentNullException(nameof(ProjectId), "Project ID is not of type Guid");
            }
        }
        
        public CodeMashSettings(string apiKey, Guid projectId)
        {
            SecretKey = apiKey;
            ProjectId = projectId;
        }
    }
}