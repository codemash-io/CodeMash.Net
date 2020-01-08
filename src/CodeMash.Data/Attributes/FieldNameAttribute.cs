using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNameAttribute : BsonElementAttribute
    {
        public FieldNameAttribute(string value) : base(value) {}
    }
}