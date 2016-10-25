using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace CodeMash.Net
{
    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public class EntityBase
    {
        [JsonProperty(PropertyName = "_id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
        [BsonElement("createdon")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}