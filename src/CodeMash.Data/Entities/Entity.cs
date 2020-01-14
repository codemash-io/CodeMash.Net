using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CodeMash.Models
{
    public class Entity : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [UniqueName("_id")]
        public string Id { get; set; }
    }
}