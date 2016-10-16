﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace CodeMash.Net.DataContracts
{
    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class EntityBase
    {
        public EntityBase()
        {
            CreatedOn = DateTime.Now;
        }

        [JsonProperty(PropertyName = "_id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; protected set; }

        [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
        [BsonElement("createdon")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}