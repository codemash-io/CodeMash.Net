using System.Runtime.Serialization;

namespace CodeMash.Net.Tests.DataContracts
{
    /// <summary>
    /// DataContract  - projection of Project class, stores only ResourceCategories property
    /// 
    /// </summary>
    [DataContract]
    public class ProjectAggregatedDataContract : BaseDataContract
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int CategoriesCount { get; set; }
    }
}