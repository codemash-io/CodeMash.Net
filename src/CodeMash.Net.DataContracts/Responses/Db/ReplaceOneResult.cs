using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class ReplaceOneResult
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

        public static explicit operator ReplaceOneResult(MongoDB.Driver.ReplaceOneResult source)
        {
            if (source == null)
            {
                return null;
            }
            
            var replaceOneResult = new ReplaceOneResult
            {
                IsAcknowledged = source.IsAcknowledged,
                MatchedCount = source.MatchedCount,
                ModifiedCount = source.ModifiedCount,
                UpsertedId = source.UpsertedId,
                IsModifiedCountAvailable = source.IsModifiedCountAvailable
            };
            return replaceOneResult;
        }

    }   
}