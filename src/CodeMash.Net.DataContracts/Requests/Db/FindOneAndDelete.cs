using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class FindOneAndDelete : RequestBase
    {
        [DataMember]
        public BsonDocument Document { get; set; }
        
        [DataMember]
        public string Filter { get; set; }

        [DataMember]
        public FindOneAndDeleteOptions<BsonDocument> FindOneAndDeleteOptions { get; set; }
        
        [DataMember]
        public Notification Notification { get; set; }
    }
}