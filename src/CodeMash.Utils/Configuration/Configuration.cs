

using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CodeMash.Utils
{
    public class Configuration
    {
        /*private static readonly Lazy<CodeMashConfigurationSection> Section = new Lazy<CodeMashConfigurationSection>(() =>
            (CodeMashConfigurationSection)ConfigurationManager.GetSection("CodeMash"));*/


        static public IConfigurationRoot ConfigurationRoot { get; set; }

        private static IConfigurationRoot AssertIfConfigurationIsSetProperlyOnJsonFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            
            
            ConfigurationRoot = builder.Build();
            

            var section = ConfigurationRoot["CodeMash"];
            if (string.IsNullOrWhiteSpace(section))
            {
                throw new Exception("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }
           

            if (string.IsNullOrWhiteSpace(ConfigurationRoot["CodeMash:ApiKey"]))
            {
                throw new Exception("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationRoot["CodeMash:Address"]))
            {
                throw new Exception("Please specify endpoint address of CodeMash in Codemash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }
            return ConfigurationRoot;
        }

        public static string ApiKey
        {
            get
            {
                var config = AssertIfConfigurationIsSetProperlyOnJsonFile();
                return config["CodeMash:ApiKey"];

            }
        }

        public static string Address 
        {
            get
            {
                var config = AssertIfConfigurationIsSetProperlyOnJsonFile();
                return config["CodeMash:Address"];
            }
            
        }

        public static string ProjectId
        {
            get
            {
                var config = AssertIfConfigurationIsSetProperlyOnJsonFile();
                return config["CodeMash:ProjectId"];
            }

        }
    }
}