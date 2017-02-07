using System.Runtime.Serialization;

namespace CodeMash.ServiceModel
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public string DatabaseConnectionString { get; set; }

    }
}
