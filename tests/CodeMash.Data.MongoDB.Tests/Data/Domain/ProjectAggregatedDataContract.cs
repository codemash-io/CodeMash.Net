using CodeMash.Data;

namespace CodeMash.Tests.Data
{
    public class ProjectAggregatedDataContract : Entity
    {        
        public string Name { get; set; }        
        public int CategoriesDistinct { get; set; }
    }
}