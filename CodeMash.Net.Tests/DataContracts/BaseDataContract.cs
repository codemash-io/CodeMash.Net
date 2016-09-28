using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using Newtonsoft.Json;

namespace CodeMash.Net.Tests
{
    [DataContract]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class BaseDataContract 
	{
        //[DataMember]
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        /*[BsonId]
        public ObjectId ObjectId;

        [BsonElement("id")]
        public string Id
        {
            get
            {
                return ObjectId.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value != "String" && value[0] != '-')
                {
                    ObjectId = new ObjectId(value);
                }
            }
        }*/

        [JsonProperty(PropertyName = "_id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; protected set; }
        
        [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
        [BsonElement("createdon")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }

}