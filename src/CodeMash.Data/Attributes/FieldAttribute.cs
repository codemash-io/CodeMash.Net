using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : BsonElementAttribute
    {
        public FieldAttribute(string value) : base(value) {}
    }
}