using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CodeMash.Net.Tests
{
    [DataContract]
    public class ResourceValue 
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public ObjectId ResourceLanguageId { get; set; }
	}
}
