using System;
using System.Configuration;

namespace CodeMash.Net
{
    public static class Statics
    {
        /*private static readonly Lazy<CodeMashConfigurationSection> Section = new Lazy<CodeMashConfigurationSection>(() =>
            (CodeMashConfigurationSection)ConfigurationManager.GetSection("CodeMash"));*/

        public static string ApiKey
        {
            get
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

                // if apiKey came form template, throw an exception
                if (codeMashConfigSection.Client.ApiKey == "Your ApiKey goes here")
                {
                    throw new ConfigurationErrorsException("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
                }

                return codeMashConfigSection.Client.ApiKey;
            }
        }

        public static string Address 
        {
            get
            {
                var section = ConfigurationManager.GetSection("CodeMash");
                if (section == null)
                {
                    throw new ConfigurationErrorsException("Please specify address in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
                }

                var codeMashConfigSection = section as CodeMashConfigurationSection;

                if (codeMashConfigSection == null)
                {
                    throw new ConfigurationErrorsException("CodeMash configuration is not set properly. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
                }
                return codeMashConfigSection.Client.Address;
            }
            
        }
    }
}