using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [CollectionName("sdk-collection")]
    public class SdkEntity : Entity
    {
        public int Number { get; set; }
        
        public string Notes { get; set; }
    }
}