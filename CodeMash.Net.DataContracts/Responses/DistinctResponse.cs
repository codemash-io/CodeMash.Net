using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Net.DataContracts
{
    public class DistinctResponse : ResponseBase<List<string>>
    {
        
#if DEBUG

        [BsonElement("filter")]
        [DataMember(Name="filter")]
        public string Filter { get; set; }
#endif
    }
}