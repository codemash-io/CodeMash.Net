using CodeMash.Data.MongoDB;

namespace CodeMash.Data.MongoDB.Tests.Data
{
    public class ProjectAggregatedDataContract : Entity
    {        
        public string Name { get; set; }        
        public int CategoriesDistinct { get; set; }
    }
}