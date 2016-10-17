using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CodeMash.Net.DataContracts;

namespace CodeMash.Net.Tests
{
    [DataContract]
    public class ResourceLanguage : EntityBase
	{
        public ResourceLanguage()
        {
            DefaultValues = new List<ResourceValue>();
        }
        [DataMember]
        public string CultureCode { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NativeName { get; set; }
        [DataMember]
        public List<ResourceValue> DefaultValues { get; set; }
	}
}
