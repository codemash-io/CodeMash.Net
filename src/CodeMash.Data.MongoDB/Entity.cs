using System;
using System.Runtime.Serialization;
using CodeMash.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Data.MongoDB
{
    public class Entity : EntityBase, IEntityWithTenant, IEntityWithResponsibleUser, IEntityWithModifiedOn
    {
        [DataMember]
        public string TenantId { get; set; }

        [DataMember]
        public string ResponsibleUserId { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.String)]
        [BsonElement("modifiedOn")]
        [DataMember]
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}
