using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class UpdateMany : RequestBase
    {
        [DataMember]
        public string Update { get; set; }

        [DataMember]
        public string SchemaId { get; set; }

        [DataMember]
        public Notification Notification { get; set; }

        [DataMember]
        public UpdateOptions UpdateOptions { get; set; }

        [DataMember]
        public string Filter { get; set; }
    }
}