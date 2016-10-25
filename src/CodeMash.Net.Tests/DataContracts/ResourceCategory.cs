using System.Collections.Generic;
using System.Runtime.Serialization;
namespace CodeMash.Net.Tests
{
    [DataContract]
    public class ResourceCategory
	{
        public ResourceCategory()
        {
            Keys = new List<ResourceKey>();
        }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<ResourceKey> Keys { get; set; }
        
	}
}
