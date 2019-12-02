using System;
using System.Collections.Generic;
using System.IO;
using CodeMash.Interfaces;
using CodeMash.Common;
using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace CodeMash.Repository
{
    public class CodeMashDatabaseSettings : ICodeMashDatabaseSettings
    {
        public const string ApiUrl = "http://api.codemash.io/";

        public CodeMashDatabaseSettings(IConfigurationRoot config, string settingsFileName = "appsettings.json")
        {
            if (config == null)
            {
                config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(settingsFileName).Build();
            }
            
            ApiKey = config["CodeMash:ApiKey"];
            Region = config["CodeMash:Region"];
            if (string.IsNullOrEmpty(Region))
            {
                Region = "eu-west-1";
            }

            var projectIdParsed = Guid.TryParse(config["CodeMash:ProjectId"], out var parsedProjectId);
            if (projectIdParsed) ProjectId = parsedProjectId;
            else
            {
                throw new ArgumentNullException(nameof(ProjectId), "Project ID is not of type Guid");
            }
        }
        
        public CodeMashDatabaseSettings(string apiKey, Guid projectId, string region = null)
        {
            ApiKey = apiKey;
            ProjectId = projectId;
            
            Region = region;
            if (string.IsNullOrEmpty(Region)) Region = "eu-west-1";
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

        public string Region { get; }

        public string ApiKey { get; }

        public Guid ProjectId { get; }
    }
}