using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueNameAttribute : BsonElementAttribute
    {
        public UniqueNameAttribute(string value) : base(value) {}
    }
}