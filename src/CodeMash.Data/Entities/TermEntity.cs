using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CodeMash.Models
{
    public class TermEntity<T> 
    {
        public string TaxonomyId { get; set; }
        
        public string TaxonomyName { get; set; }
        
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public Dictionary<string, string> Names { get; set; }
        
        public Dictionary<string, string> Descriptions { get; set; }
        
        public string ParentId { get; set; }
        
        public string ParentName { get; set; }
        
        public Dictionary<string, string> ParentNames { get; set; }
        
        public int? Order { get; set; }
        
        public Dictionary<string, List<string>> Dependencies { get; set; }
        
        public T Meta { get; set; }
    }
}