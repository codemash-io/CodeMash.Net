using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CodeMash.Net.DataContracts;

namespace CodeMash.Net.Tests
{
    public class User : EntityBase
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public List<ObjectId> AccessibleProjects { get; set; }
    }
}