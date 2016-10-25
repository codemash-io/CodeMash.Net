using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class FindOneAndReplace : RequestBase
    {
        [DataMember]
        public string Document { get; set; }
        
        [DataMember]
        public string Filter { get; set; }

        [DataMember]
        public FindOneAndReplaceOptions<BsonDocument> FindOneAndReplaceOptions { get; set; }
        
        [DataMember]
        public Notification Notification { get; set; }
    }
}