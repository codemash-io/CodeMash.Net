using System.Collections.Generic;
using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-translatable-nested")]
    public class TranslatableWithNestedEntity : Entity
    {
        public string NonTranslatable { get; set; }
        
        public List<TranslatableEntity> NestedTranslatable { get; set; }
    }
}