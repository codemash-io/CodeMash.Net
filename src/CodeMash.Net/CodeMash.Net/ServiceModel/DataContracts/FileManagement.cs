using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash
{
    public class FileManagement
    {
        [BsonElement("ftpAddress")]
        public string FtpAddress { get; set; }
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("port")]
        public string Port { get; set; }
        [BsonElement("folder")]
        public string FolderName { get; set; }
    }
}