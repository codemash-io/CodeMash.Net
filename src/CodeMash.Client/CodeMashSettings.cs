using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace CodeMash.Client
{
    public class CodeMashSettings : ICodeMashSettings
    {
        public const string ApiUrl = "http://api.codemash.io/";

        public CodeMashSettings(IConfigurationRoot config, string settingsFileName = "appsettings.json")
        {
            if (config == null)
            {
                config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(settingsFileName).Build();
            }
            
            ApiKey = config["CodeMash:ApiKey"];

            var projectIdParsed = Guid.TryParse(config["CodeMash:ProjectId"], out var parsedProjectId);
            if (projectIdParsed) ProjectId = parsedProjectId;
            else
            {
                throw new ArgumentNullException(nameof(ProjectId), "Project ID is not of type Guid");
            }
        }
        
        public CodeMashSettings(string apiKey, Guid projectId)
        {
            ApiKey = apiKey;
            ProjectId = projectId;
        }

        public IServiceClient Client
        {
            get
            {
                var client = new JsonServiceClient(ApiUrl);
                if (ProjectId != Guid.Empty)
                {
                    client.Headers.Add("X-CM-ProjectId", this.ProjectId.ToString());
                }

                if (!string.IsNullOrEmpty(ApiKey))
                {
                    client.BearerToken = ApiKey;
                }
                  
                return client.WithCache();
            }
        }

        public string ApiKey { get; }

        public Guid ProjectId { get; }
    }
}