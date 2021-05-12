using System;
using System.Collections.Generic;
using CodeMash.Models;
using Newtonsoft.Json;

namespace CodeMash.Core.Tests
{
    public class AggregateProjection
    {
        [Field("field_1")]
        public string Attribute1 { get; set; }
        
        [Field("field_2")]
        public string Attribute2 { get; set; }
        
        [Field("single")]
        public NestedAggregateClass Single { get; set; }
        
        [Field("multi")]
        public List<NestedAggregateClass> Multi { get; set; }
    }

    public class NestedAggregateClass
    {
        [Field("d")]
        public DateTime Date { get; set; }
        
        [JsonProperty("t")]
        public string Text { get; set; }
        
        [Field("n")]
        public NestedAggregateClass Nest { get; set; }
    }
}