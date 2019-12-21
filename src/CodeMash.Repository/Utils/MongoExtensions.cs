using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    public static class MongoExtensions
    {
        public static string FilterToJson<T>(this FilterDefinition<T> filter)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
        }
        
        public static string ProjectionToJson<T, TP>(this ProjectionDefinition<T, TP> projection)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            var projectionInfo = projection.Render(documentSerializer, BsonSerializer.SerializerRegistry);
            return projectionInfo.Document?.ToString();
        }
        
        public static string SortToJson<T>(this SortDefinition<T> sort)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return sort.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
        }
    }
}