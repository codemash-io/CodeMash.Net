using MongoDB.Bson;
using System.Collections.Generic;
using CodeMash.Data;

namespace CodeMash.Tests.Data
{    
    [CollectionName("Projects")]
    public class Project : Entity
    {
        public Project() : this(new List<ObjectId>(), new List<ObjectId>())
        {
        }

        public Project(List<ObjectId> availableLanguages, List<ObjectId> users )
        {
            Categories = new List<ResourceCategory>();
            SupportedLanguages = availableLanguages ?? new List<ObjectId>();
            Users = users ?? new List<ObjectId>();
        }
        
        public string Name { get; set; }        
        public string Description { get; set; }        
        public List<ObjectId> Users { get; set; }        
        public List<ResourceCategory> Categories { get; set; }        
        public List<ObjectId> SupportedLanguages { get; set; }         
        public string ImageId { get; set; }
    }

    
}