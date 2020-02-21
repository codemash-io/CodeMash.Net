using System;
using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-datetime")]
    public class DateTimeEntity : Entity
    {
        public DateTime DateTimeField { get; set; }
    }
    
    public class DateTimeNonEntity
    {
        public DateTime DateTimeField { get; set; }
    }
}