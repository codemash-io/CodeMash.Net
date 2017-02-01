using System.Runtime.Serialization;

namespace CodeMash.ServiceModel
{
    [DataContract]
    public class UserSettings
    {
        [DataMember]
        public string DatabaseConnectionString { get; set; }

    }
}
