using System;
using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-currency")]
    public class CurrencyEntity : Entity
    {
        [Field("currency")]
        public CurrencyField Currency { get; set; }
        
        [Field("array")]
        public List<CurrencyField> Currencies { get; set; }
    }
    
    public class CurrencyField
    {
        [Field("value")]
        public decimal Value { get; set; }
        
        [Field("currency")]
        public string Currency { get; set; }
    }
}