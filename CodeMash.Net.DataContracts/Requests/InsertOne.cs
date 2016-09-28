using System.Runtime.Serialization;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class InsertOne : RequestBase
    {
        [DataMember]
        public string Document { get; set; }

        [DataMember]
        public string SchemaId { get; set; }

        [DataMember]
        public InsertOneOptions InsertOneOptions { get; set; }

        [DataMember]
        public Notification Notification { get; set; }
    }



}