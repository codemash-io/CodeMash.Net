#if NET461
using System.Configuration;


namespace CodeMash
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

#endif