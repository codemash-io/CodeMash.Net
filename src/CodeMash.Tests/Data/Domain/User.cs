using MongoDB.Bson;
using System.Collections.Generic;
using CodeMash.Data;

namespace CodeMash.Tests.Data
{
    public class User : Entity
    {       
        public string Name { get; set; }        
        public string Password { get; set; }
        public List<ObjectId> AccessibleProjects { get; set; }
    }
}