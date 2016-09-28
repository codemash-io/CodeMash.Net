using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class ReplaceOne : RequestBase
    {
        [DataMember]
        public string SchemaId { get; set; }

        [DataMember]
        public string Document { get; set; }

        [DataMember]
        public Notification Notification { get; set; }

        [DataMember]
        public UpdateOptions UpdateOptions { get; set; }

        [DataMember]
        public string Filter { get; set; }
    }
}