using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Models
{
    public interface IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [UniqueName("_id")]
        string Id { get; set; }
    }
}