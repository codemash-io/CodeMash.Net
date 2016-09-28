using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Net.Tests
{
    [DataContract]
    public class ResourceKey 
	{
        public ResourceKey()
        {
            Values = new List<ResourceValue>();
        }

        [DataMember]
        public string Key { get; set; }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public List<ResourceValue> Values { get; set; }
	}
}
