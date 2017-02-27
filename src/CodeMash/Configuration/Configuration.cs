using System.Configuration;
#if NETSTANDARD1_6
using Microsoft.Extensions.Configuration;
#endif
using System;
using System.IO;

namespace CodeMash
{
    public class Configuration
    {
        /*private static readonly Lazy<CodeMashConfigurationSection> Section = new Lazy<CodeMashConfigurationSection>(() =>
            (CodeMashConfigurationSection)ConfigurationManager.GetSection("CodeMash"));*/



#if !NETSTANDARD1_6
        private static CodeMashConfigurationSection AssertIfConfigurationIsSetProperly()
        {

            var section = ConfigurationManager.GetSection("CodeMash");
            if (section == null)
            {
                throw new ConfigurationErrorsException("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }

            var codeMashConfigSection = section as CodeMashConfigurationSection;

            if (codeMashConfigSection == null)
            {
                throw new ConfigurationErrorsException("CodeMash configuration is not set properly. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }

            if (string.IsNullOrWhiteSpace(codeMashConfigSection.Client.ApiKey))
            {
                throw new ConfigurationErrorsException("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }

            if (string.IsNullOrWhiteSpace(codeMashConfigSection.Client.Address))
            {
                throw new ConfigurationErrorsException("Please specify endpoint address of CodeMash in Codemash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }
            return codeMashConfigSection;
        }
#else

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
                throw new ConfigurationErrorsException("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }


            if (string.IsNullOrWhiteSpace(ConfigurationRoot["CodeMash:Client:ApiKey"]))
            {
                throw new ConfigurationErrorsException("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationRoot["CodeMash:Client:Address"]))
            {
                throw new ConfigurationErrorsException("Please specify endpoint address of CodeMash in Codemash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }
            return ConfigurationRoot;
        }
#endif
        public static string ApiKey
        {
            get
            {
#if !NETSTANDARD1_6
                var config = AssertIfConfigurationIsSetProperly();
                return config.Client.ApiKey;
#else
                var config = AssertIfConfigurationIsSetProperlyOnJsonFile();
                return ConfigurationRoot["CodeMash:Client:ApiKey"];
#endif
            }
        }

        public static string Address 
        {
            get
            {
#if !NETSTANDARD1_6
                var config = AssertIfConfigurationIsSetProperly();
                return config.Client.Address;
#else
                var config = AssertIfConfigurationIsSetProperlyOnJsonFile();
                return ConfigurationRoot["CodeMash:Client:Address"];
#endif
            }
            
        }
    }
}