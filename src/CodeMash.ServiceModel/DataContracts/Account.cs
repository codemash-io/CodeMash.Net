using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.ServiceModel
{
    
    public class Account : BaseDataContract
    {
        [BsonElement("companyName")]
        public string CompanyName { get; set; }

        [BsonElement("dataBase")]
        public DatabaseManagement DataBase { get; set; }

        [BsonElement("plan")]
        public PlanManagement Plan { get; set; }

        [BsonElement("file")]
        public FileManagement File { get; set; }

        [BsonElement("email")]
        public EmailManagement Email { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("isEmailConfirmed")]
        public bool IsEmailConfirmed { get; set; }
    }
}
