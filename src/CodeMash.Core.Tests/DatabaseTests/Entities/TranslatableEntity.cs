using System.Collections.Generic;
using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-translatable")]
    public class TranslatableEntity : Entity
    {
        public string NonTranslatable { get; set; }
        
        public Dictionary<string, string> Translatable { get; set; }
    }
}