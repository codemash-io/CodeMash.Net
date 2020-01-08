using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [CollectionName("sdk-translatable")]
    public class TranslatableEntity : Entity
    {
        [FieldName("nonTranslatable")]
        public string NonTranslatable { get; set; }
        
        [FieldName("translatable")]
        public Dictionary<string, string> Translatable { get; set; }
    }
}