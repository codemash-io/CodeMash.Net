using System.Collections.Generic;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    // For sorting and projection, use classes
    // CollectionReferenceExpressionField or CollectionReferenceBuilderField
    public class CollectionReferenceField<T>
    {
        // Field name
        public string Name { get; set; }
        
        // Only for Find request (not FindOne)
        // Max allowed - 100
        public int PageSize { get; set; } = 100;
        
        // Only for Find request (not FindOne)
        public int PageNumber { get; set; }
        
        public bool Excluded { get; set; }
        
        // Only for Find request (not FindOne)
        public SortDefinition<T> Sort { get; set; }
        
        public ProjectionDefinition<T> Projection { get; set; }
    }
}