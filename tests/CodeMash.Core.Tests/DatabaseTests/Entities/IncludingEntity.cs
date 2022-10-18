using System.Collections.Generic;
using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-including-entity")]
    public class IncludingEntity : Entity
    {
        [Field("text")]
        public string Text { get; set; }
        
        [Field("taxref")]
        public IncludingNameId TaxRef { get; set; }
        
        [Field("nest")]
        public List<NestedIncluding> TaxRefs { get; set; }
    }

    public class NestedIncluding
    {
        [Field("taxref")]
        public IncludingNameId TaxRef { get; set; }
    }

    public class IncludingNameId
    {
        [Field("id")]
        public string Date { get; set; }
        
        [Field("name")]
        public string Nested { get; set; }
    }
}