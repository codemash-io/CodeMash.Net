using System;
using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-n-referencing-entity")]
    public class ReferencingFromNestedEntity : Entity
    {
        [Field("text")]
        public string Text { get; set; }
        
        [Field("nested")]
        public List<NestedWithReference> Nested { get; set; }
    }
    public class NestedWithReference
    {
        [Field("singleref")]
        public ReferencingEntity NestedSingleRef { get; set; }
        
        [Field("multiref")]
        public List<ReferencingEntity> NestedMultiRef { get; set; }
    }
}