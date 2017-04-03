using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash
{
    public class DatabaseManagement
    {
        [BsonElement("dataBaseName")]
        public string DataBaseName { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("usedStorage")]
        public double UsedStorage { get; set; }

        [BsonElement("connectionString")]
        public string ConnectionString { get; set; }
    }
}