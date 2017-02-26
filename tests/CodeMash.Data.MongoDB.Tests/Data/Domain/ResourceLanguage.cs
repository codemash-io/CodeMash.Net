using System.Collections.Generic;
using CodeMash.Data;

namespace CodeMash.Tests.Data
{
    
    public class ResourceLanguage : Entity
    {
        public ResourceLanguage()
        {
            DefaultValues = new List<ResourceValue>();
        }
        
        public string CultureCode { get; set; }        
        public string Name { get; set; }        
        public string NativeName { get; set; }        
        public List<ResourceValue> DefaultValues { get; set; }
	}
}
