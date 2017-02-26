using System.Collections.Generic;

namespace CodeMash.Tests.Data
{    
    public class ResourceCategory
	{
        public ResourceCategory()
        {
            Keys = new List<ResourceKey>();
        }
        
        public string Key { get; set; }        
        public string Name { get; set; }        
        public List<ResourceKey> Keys { get; set; }        
	}
}
