using System;
using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-referencing-entity")]
    public class ReferencingEntity : Entity
    {
        [Field("date")]
        public DateTime Date { get; set; }
        
        [Field("text")]
        public string Text { get; set; }
        
        [Field("singleRef")]
        public SingleReferenceEntity SingleRef { get; set; }
        
        [Field("multiRef")]
        public List<MultiReferenceEntity> MultiRef { get; set; }
    }

    public class SingleReferenceEntity : Entity
    {
        public DateTime Date { get; set; }
        
        public List<NestedInReferenced> Nested { get; set; }
    }
    
    public class NestedInReferenced
    {
        [Field("date")]
        public DateTime Date { get; set; }
        
        [Field("number")]
        public int Number { get; set; }
    }
    
    public class MultiReferenceEntity : Entity
    {
        public DateTime Date { get; set; }
        
        public List<NestedInReferenced> Nested { get; set; }
    }
}