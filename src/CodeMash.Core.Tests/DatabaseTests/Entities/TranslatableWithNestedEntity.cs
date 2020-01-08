using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [CollectionName("sdk-translatable-nested")]
    public class TranslatableWithNestedEntity : Entity
    {
        [FieldName("nonTranslatable")]
        public string NonTranslatable { get; set; }
        
        [FieldName("nestedTranslatable")]
        public List<TranslatableEntity> NestedTranslatable { get; set; }
    }
}