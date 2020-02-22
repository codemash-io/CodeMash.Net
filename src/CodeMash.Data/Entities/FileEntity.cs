using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CodeMash.Models
{
    public class FileEntity : IEntity
    {
        [Field("_id")]
        [JsonProperty("_id")]
        public string Id { get; set; }
        
        [Field("directory")]
        [JsonProperty("directory")]
        public string Directory { get; set; }
        
        [Field("originalFileName")]
        [JsonProperty("originalFileName")]
        public string OriginalFileName { get; set; }
        
        [Field("fileName")]
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        
        [Field("contentType")]
        [JsonProperty("contentType")]
        public string ContentType { get; set; }
        
        [Field("contentLength")]
        [JsonProperty("contentLength")]
        public long ContentLength { get; set; }
        
        [Field("optimizations")]
        [JsonProperty("optimizations")]
        public List<FileOptimization> Optimizations { get; set; }
    }
    
    public class FileOptimization
    {
        [Field("optimizedFileId")]
        [JsonProperty("optimizedFileId")]
        public string OptimizedFileId { get; set; }
        
        [Field("optimization")]
        [JsonProperty("optimization")]
        public string Optimization { get; set; }
        
        [Field("directory")]
        [JsonProperty("directory")]
        public string Directory { get; set; }
        
        [Field("originalFileName")]
        [JsonProperty("originalFileName")]
        public string OriginalFileName { get; set; }
        
        [Field("fileName")]
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        
        [Field("contentType")]
        [JsonProperty("contentType")]
        public string ContentType { get; set; }
        
        [Field("contentLength")]
        [JsonProperty("contentLength")]
        public long ContentLength { get; set; }
    }
}