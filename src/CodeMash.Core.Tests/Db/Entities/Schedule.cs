using CodeMash.Interfaces;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Core.Tests
{
    [CollectionName("trains-schedule")]
    public class Schedule : Entity, IEntity
    {
        [BsonElement("number")]
        public int Number { get; set; }
        
        [BsonElement("origin")]
        public string Origin { get; set; }
        
        [BsonElement("destination")]
        public string Destination { get; set; }
        
        [BsonElement("notes")]
        public string Notes { get; set; }
    }
    
}