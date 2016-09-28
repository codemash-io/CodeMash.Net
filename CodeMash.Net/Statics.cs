using System;
using System.Configuration;

namespace CodeMash.Net
{
    public static class Statics
    {
        /*private static readonly Lazy<CodeMashConfigurationSection> Section = new Lazy<CodeMashConfigurationSection>(() =>
            (CodeMashConfigurationSection)ConfigurationManager.GetSection("CodeMash"));*/
        
        public static string ApiKey => ((CodeMashConfigurationSection)ConfigurationManager.GetSection("CodeMash")).Client.ApiKey;

        public static string Address => ((CodeMashConfigurationSection)ConfigurationManager.GetSection("CodeMash")).Client.Address;
    }
}