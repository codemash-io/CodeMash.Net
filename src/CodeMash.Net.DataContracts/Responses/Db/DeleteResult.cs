using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace CodeMash.Net.DataContracts
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class DeleteResult
    {
        [BsonElement("deletedCount")]
        [DataMember(Name = "deletedCount")]
        public long DeletedCount { get; set; }
        [BsonElement("isAcknowledged")]
        [DataMember(Name = "isAcknowledged")]
        public bool IsAcknowledged { get; set; }

        public static explicit operator DeleteResult(MongoDB.Driver.DeleteResult source)
        {
            if (source == null)
            {
                return null;
            }

            var deleteResult = new DeleteResult
            {
                IsAcknowledged = source.IsAcknowledged,
                DeletedCount = source.DeletedCount
            };
            return deleteResult;
        }
    }
}
