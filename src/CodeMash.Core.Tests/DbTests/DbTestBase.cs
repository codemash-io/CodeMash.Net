using CodeMash.Interfaces;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Core.Tests
{
    public class DbTestBase{
        [CollectionName("trains")]
        public class Train : Entity, IEntity
        {
            [BsonElement("number")]
            public string Number { get; set; }
            [BsonElement("origin")]
            public string Origin { get; set; }
            [BsonElement("destination")]
            public string Destination { get; set; }
        }

        // 15 Kaunas Vilnius
        // 140 Vilnius Kaunas
        // 410 Kaunas Klaipeda
        // 1454 Vilnius Trakai
    }
    
}