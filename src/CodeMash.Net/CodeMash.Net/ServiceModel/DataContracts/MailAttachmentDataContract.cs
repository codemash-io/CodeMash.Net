using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash
{
    [DataContract(Name = "MailAttachment")]
    [BsonIgnoreExtraElements]
    public class MailAttachmentDataContract
    {
        public MailAttachmentDataContract(string attachmentName, byte[] contentStream)
        {
            FileName = attachmentName;
            Data = contentStream;
        }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] Data { get; set; }
    }
}
