using System.Runtime.Serialization;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class UpdateOne : RequestBase
    {
        public UpdateOne()
        {
            Filter = "{}";
        }

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