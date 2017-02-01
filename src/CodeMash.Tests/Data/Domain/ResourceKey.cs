using System.Collections.Generic;

namespace CodeMash.Tests.Data
{
    
    public class ResourceKey 
	{
        public ResourceKey()
        {
            Values = new List<ResourceValue>();
        }                
        public string Key { get; set; }                
        public string Name { get; set; }                
        public List<ResourceValue> Values { get; set; }
	}
}
