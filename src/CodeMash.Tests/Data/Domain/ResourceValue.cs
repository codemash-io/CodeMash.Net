using MongoDB.Bson;

namespace CodeMash.Tests.Data
{
    public class ResourceValue 
    {       
        public string Value { get; set; }
        public ObjectId ResourceLanguageId { get; set; }
	}
}
