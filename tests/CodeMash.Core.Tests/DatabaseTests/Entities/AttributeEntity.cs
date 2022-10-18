using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-attribute")]
    public class AttributeEntity : Entity
    {
        [Field("field_1")]
        public string Attribute1 { get; set; }
        
        [Field("field_2")]
        public int Attribute2 { get; set; }
        
        public int Attribute3 { get; set; }
        
        public int attribute_4 { get; set; }
    }
}