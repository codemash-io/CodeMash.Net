using MongoDB.Bson;
using System.Collections.Generic;

namespace CodeMash.Data.MongoDB.Tests.Data
{    
    [CollectionName("Projects")]
    public class Project : Entity
    {
        public Project() : this(new List<string>(), new List<ObjectId>())
        {
        }

        public Project(List<string> availableLanguages, List<ObjectId> users )
        {
            Categories = new List<ResourceCategory>();
            SupportedLanguages = availableLanguages ?? new List<string>();
            Users = users ?? new List<ObjectId>();
        }
        
        public string Name { get; set; }        
        public string Description { get; set; }        
        public List<ObjectId> Users { get; set; }        
        public List<ResourceCategory> Categories { get; set; }        
        public List<string> SupportedLanguages { get; set; }         
        public string ImageId { get; set; }
    }

    
}