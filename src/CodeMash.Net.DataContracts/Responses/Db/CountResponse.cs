using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Net.DataContracts
{
    public class CountResponse : ResponseBase<long>
    {
        public string Schema { get; set; }

#if DEBUG

        [BsonElement("filter")]
        [DataMember(Name="filter")]
        public string Filter { get; set; }
#endif
    }
}