using MongoDB.Bson;
using System.Collections.Generic;

namespace CodeMash.Data.MongoDB.Tests.Data
{
    public class User : Entity
    {       
        public string Name { get; set; }        
        public string Password { get; set; }
        public List<ObjectId> AccessibleProjects { get; set; }
    }
}