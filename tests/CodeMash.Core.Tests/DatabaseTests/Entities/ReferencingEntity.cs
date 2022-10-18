using System;
using System.Collections.Generic;
using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-referencing-entity")]
    public class ReferencingEntity : Entity
    {
        [Field("date")]
        public DateTime Date { get; set; }
        
        [Field("text")]
        public string Text { get; set; }
        
        [Field("singleref")]
        public ReferencedEntity SingleRef { get; set; }
        
        [Field("multiref")]
        public List<ReferencedEntity> MultiRef { get; set; }
        
        [Field("singletaxref")]
        public TermEntity<ReferencedTermMeta> SingleTaxRef { get; set; }
        
        [Field("multitaxref")]
        public List<TermEntity<ReferencedTermMeta>> MultiTaxRef { get; set; }
        
        [Field("files")]
        public List<FileEntity> Files { get; set; }
    }

    public class ReferencedEntity : Entity
    {
        [Field("date")]
        public DateTime Date { get; set; }
        
        [Field("nested")]
        public List<NestedInReferenced> Nested { get; set; }
    }
    
    public class NestedInReferenced
    {
        [Field("date")]
        public DateTime Date { get; set; }
        
        [Field("number")]
        public int Number { get; set; }
    }
    
    public class ReferencedTermMeta
    {
        [Field("text")]
        public string Text { get; set; }
    }
}