using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Core.Tests
{
    [CollectionName("sdk-attribute")]
    public class AttributeEntity : Entity
    {
        [FieldName("field_1")]
        public string Attribute1 { get; set; }
        
        [FieldName("field_2")]
        public int Attribute2 { get; set; }
        
        public int Attribute3 { get; set; }
        
        public int attribute_4 { get; set; }
    }
}