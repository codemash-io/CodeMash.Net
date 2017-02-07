using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.ServiceModel
{
    public class EmailManagement
    {
        [BsonElement("freeMonthlyEmails")]
        public int FreeMonthlyEmails { get; set; }

        [BsonElement("sentEmailsInCurrentMonth")]
        public int SentEmailsInCurrentMonth { get; set; }
    }
}