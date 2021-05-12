using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    public class CollectionReferenceField
    {
        /// <summary>
        /// Field name to apply the following parameters on
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Amount of referenced records to get
        /// Only for Find request (not FindOne), max allowed - 100
        /// </summary>
        public int PageSize { get; set; } = 100;
        
        /// <summary>
        /// This field multiplied by PageSize tells how much referenced records to skip
        /// Only for Find request (not FindOne)
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// Tells how to sort array of referenced records
        /// Use SetSort method to set this field
        /// Only for Find request (not FindOne)
        /// </summary>
        private string _sort { get; set; }
        
        /// <summary>
        /// Tells what fields to include/exclude in referenced records
        /// Use SetProjection method to set this field
        /// </summary>
        private string _projection { get; set; }
        
        public void SetProjection<T>(ProjectionDefinition<T> projection)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            var projectionInfo = projection.Render(documentSerializer, BsonSerializer.SerializerRegistry);
            _projection = projectionInfo?.ToString();
        }
        
        public void SetSort<T>(SortDefinition<T> sort)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            _sort = sort.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
        }

        public string GetSort => _sort;
        
        public string GetProjection => _projection;
    }
}