using CodeMash.Net.DataContracts;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeMash.Net.Tests
{
    [DataContract]
    public class Project : EntityBase
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
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<ObjectId> Users { get; set; }
        [DataMember]
        public List<ResourceCategory> Categories { get; set; }

        [DataMember]
        public List<ObjectId> SupportedLanguages { get; set; }    

        [DataMember]
        public string ImageId { get; set; }
    }

    
}