using System;
using System.Runtime.Serialization;
using CodeMash.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Data.MongoDB
{
    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    [DataContract]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity<string>, IEntityWithCreatedOn
    {
        protected Entity()
        {
            CreatedOn = DateTime.Now;
        }

        [DataMember]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        [BsonDateTimeOptions(Representation = BsonType.String, Kind = DateTimeKind.Utc)]
        [BsonElement("createdOn")]
        [DataMember]
        public DateTime CreatedOn { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        private static bool IsTransient(Entity obj)
        {
            return obj != null && (obj.Id == string.Empty || ObjectId.Parse(obj.Id) == ObjectId.Empty);
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }
        
        public virtual bool Equals(Entity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (IsTransient(this) || IsTransient(other) || !Equals(Id, other.Id)) 
                return false;
            
            var otherType = other.GetUnproxiedType();
            var thisType = GetUnproxiedType();
            return thisType.IsAssignableFrom(otherType) ||
                   otherType.IsAssignableFrom(thisType);

        }
        

        public override int GetHashCode()
        {
            return Id == string.Empty ? base.GetHashCode() : Id.GetHashCode();
        }

        public static bool operator ==(Entity x, Entity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Entity x, Entity y)
        {
            return !(x == y);
        }
    }
}
