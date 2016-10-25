using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Net.DataContracts
{
    public class AggregateResponse : ResponseBase<string>
    {

#if DEBUG

        [BsonElement("aggregation")]
        [DataMember(Name="aggregation")]
        public string Aggregation { get; set; }
#endif

    }
}