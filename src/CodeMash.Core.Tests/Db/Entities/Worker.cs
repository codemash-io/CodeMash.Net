using System;
using CodeMash.Interfaces;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Core.Tests
{
    [CollectionName("workers")]
    public class Worker : Entity, IEntity
    {
        [BsonElement("yearlySalary")]
        public int Salary { get; set; }
        
        [BsonElement("personalId")]
        public string PersonalId { get; set; }
        
        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }
    }
    
}