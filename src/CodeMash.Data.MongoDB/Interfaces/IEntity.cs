using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Data.MongoDB
{
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }
    }

    public interface IEntity : IEntity<string>
    {
    }
}

