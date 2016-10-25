using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeMash.Net.Tests.DataContracts
{

    /// <summary>
    /// DataContract  - projection of Project class, stores only ResourceCategories property
    /// 
    /// </summary>
    [DataContract]
    public class ProjectProjectionDataContract
    {
        [DataMember]
        public List<ResourceCategory> Categories { get; set; }
    }
}
