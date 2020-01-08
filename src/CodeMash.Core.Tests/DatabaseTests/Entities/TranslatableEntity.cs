using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [CollectionName("sdk-translatable")]
    public class TranslatableEntity : Entity
    {
        public string NonTranslatable { get; set; }
        
        public Dictionary<string, string> Translatable { get; set; }
    }
}