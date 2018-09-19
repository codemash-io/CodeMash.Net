#if NET461
using System.Configuration;


namespace CodeMash.Utils
{
    public class CodeMashConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("apiKey", IsRequired = true)]
        public string ApiKey
        {
            get { return (string)this["apiKey"]; }
            set { this["apiKey"] = value; }
        }
        
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }     
        
        [ConfigurationProperty("projectId", IsRequired = false)]
        public string ProjectId
        {
            get { return (string)this["projectId"]; }
            set { this["projectId"] = value; }
        }    
    }
}

#endif