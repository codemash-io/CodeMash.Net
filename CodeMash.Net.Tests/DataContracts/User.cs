using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeMash.Net.Tests
{
    public class User : BaseDataContract
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public List<ObjectId> AccessibleProjects { get; set; }
    }
}