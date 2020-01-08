using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [CollectionName("sdk-collection")]
    public class SdkEntity : Entity
    {
        [FieldName("number")]
        public int Number { get; set; }
        
        [FieldName("notes")]
        public string Notes { get; set; }
    }
}