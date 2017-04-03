using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;


namespace CodeMash
{
    [DataContract]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [BsonIgnoreExtraElements]
    public class BaseDataContract
    {
        [JsonProperty(PropertyName = "id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; protected set; }

        [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}