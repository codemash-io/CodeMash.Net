using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Net.DataContracts
{
    public class FindOneResponse : ResponseBase<string>
    {
        public string Schema { get; set; }

#if DEBUG

        [BsonElement("filter")]
        [DataMember(Name="filter")]
        public string Filter { get; set; }
        [BsonElement("projection")]
        [DataMember(Name="projection")]
        public string Projection { get; set; }

#endif
    }
}