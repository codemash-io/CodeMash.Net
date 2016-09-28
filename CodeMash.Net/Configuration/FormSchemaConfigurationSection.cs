using System.Configuration;

namespace CodeMash.Net
{
    public class CodeMashConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("client")]
        public CodeMashConfigurationElement Client
        {
            get { return (CodeMashConfigurationElement)this["client"]; }
            set { this["client"] = value; }
        }
    }
}