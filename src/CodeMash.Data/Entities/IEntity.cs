using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CodeMash.Models
{
    public interface IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [Field("_id")]
        [JsonProperty("_id")]
        string Id { get; set; }
    }
}