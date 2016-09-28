using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class UpdateResult
    {
        [BsonElement("isAcknowledged")]
        [DataMember(Name = "isAcknowledged")]
        public bool IsAcknowledged { get; set; }

        [BsonElement("isModifiedCountAvailable")]
        [DataMember(Name = "isModifiedCountAvailable")]
        public bool IsModifiedCountAvailable { get; set; }

        [BsonElement("matchedCount")]
        [DataMember(Name = "matchedCount")]
        public long MatchedCount { get; set; }
        [BsonElement("modifiedCount")]
        [DataMember(Name = "modifiedCount")]
        public long ModifiedCount { get; set; }
        [BsonElement("upsertedId")]
        [DataMember(Name = "upsertedId")]
        public BsonValue UpsertedId { get; set; }

        public static explicit operator UpdateResult(MongoDB.Driver.UpdateResult source)
        {
            if (source == null)
            {
                return null;
            }
            
            var updateResponse = new UpdateResult
            {
                IsAcknowledged = source.IsAcknowledged,
                MatchedCount = source.MatchedCount,
                ModifiedCount = source.ModifiedCount,
                UpsertedId = source.UpsertedId,
                IsModifiedCountAvailable = source.IsModifiedCountAvailable
            };
            return updateResponse;
        }

    }
}
