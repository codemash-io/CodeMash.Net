using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-collection")]
    public class SdkEntity : Entity
    {
        [Field("number")]
        public int Number { get; set; }
        
        [Field("notes")]
        public string Notes { get; set; }
    }
}