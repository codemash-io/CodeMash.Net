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

        [ConfigurationProperty("binding", IsRequired = false)]
        public string Binding
        {
            get { return (string)this["binding"]; }
            set { this["binding"] = value; }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }     
        
        [ConfigurationProperty("applicationId", IsRequired = false)]
        public string ApplicationId
        {
            get { return (string)this["applicationId"]; }
            set { this["applicationId"] = value; }
        }    
    }
}

#endif