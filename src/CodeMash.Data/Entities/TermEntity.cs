using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CodeMash.Models
{
    public class TermEntity<T> : IEntity
    {
        [Field("_id")]
        [JsonProperty("_id")]
        public string Id { get; set; }
        
        [Field("taxonomyId")]
        [JsonProperty("taxonomyId")]
        public string TaxonomyId { get; set; }
        
        [Field("taxonomyName")]
        [JsonProperty("taxonomyName")]
        public string TaxonomyName { get; set; }
        
        // Set if using culture
        [Field("name")]
        [JsonProperty("name")]
        public string Name { get; set; }
        
        // Set if using culture
        [Field("description")]
        [JsonProperty("description")]
        public string Description { get; set; }
        
        // Set if not using culture
        [Field("names")]
        [JsonProperty("names")]
        public Dictionary<string, string> Names { get; set; }
        
        // Set if not using culture
        [Field("descriptions")]
        [JsonProperty("descriptions")]
        public Dictionary<string, string> Descriptions { get; set; }
        
        [Field("parentId")]
        [JsonProperty("parentId")]
        public string ParentId { get; set; }
        
        // Set if using culture
        [Field("parentName")]
        [JsonProperty("parentName")]
        public string ParentName { get; set; }
        
        // Set if not using culture
        [Field("parentNames")]
        [JsonProperty("parentNames")]
        public Dictionary<string, string> ParentNames { get; set; }
        
        [Field("order")]
        [JsonProperty("order")]
        public int? Order { get; set; }
        
        [Field("dependencies")]
        [JsonProperty("dependencies")]
        public Dictionary<string, List<string>> Dependencies { get; set; }
        
        [Field("meta")]
        [JsonProperty("meta")]
        public T Meta { get; set; }
    }
    
    public class TermEntity : TermEntity<string> {}
}