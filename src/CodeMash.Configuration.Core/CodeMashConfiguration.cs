using System;
using System.IO;
using System.Net;
using CodeMash.Interfaces;
using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace CodeMash.Configuration.Core
{
    public class CodeMashSettingsCore : ICodeMashSettings
    {
        private const string ApiKeyKey = "CodeMash:ApiKey";
        private const string ApiUrlKey = "CodeMash:ApiUrl";
        private const string ApiProjectIdKey = "CodeMash:ProjectId";
        
        public CodeMashSettingsCore(IConfigurationRoot config, string settingsFileName = "appsettings.json")
        {
            if (config == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(settingsFileName);
            
                config = builder.Build();    
            }
            
            ApiKey = config[ApiKeyKey];
            ApiUrl = config[ApiUrlKey];
            ProjectId = Guid.Parse(config[ApiProjectIdKey]);
        }
        
        public IServiceClient Client
        {
            get
            {
                var jsonClient = new JsonServiceClient(ApiUrl)
                {
                    Credentials = new NetworkCredential(ApiKey, "")
                };

                if (ProjectId != Guid.Empty)
                {
                    jsonClient.Headers.Add("X-CM-ProjectId", ProjectId.ToString());
                }
                
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    jsonClient.Headers.Add("Authorization", $"Bearer: {ApiKey}");
                }
                
                return jsonClient.WithCache();
            }
        }

        public string ApiKey { get; }
        public string ApiUrl { get; }
        public Guid ProjectId { get; }
    }
}