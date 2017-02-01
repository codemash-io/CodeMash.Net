using System.Configuration;

namespace CodeMash
{
    public class Configuration
    {
        /*private static readonly Lazy<CodeMashConfigurationSection> Section = new Lazy<CodeMashConfigurationSection>(() =>
            (CodeMashConfigurationSection)ConfigurationManager.GetSection("CodeMash"));*/

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

        public static string ApiKey
        {
            get
            {
                var config = AssertIfConfigurationIsSetProperly();
                return config.Client.ApiKey;
            }
        }

        public static string Address 
        {
            get
            {
                var config = AssertIfConfigurationIsSetProperly();
                return config.Client.Address;
            }
            
        }
    }
}