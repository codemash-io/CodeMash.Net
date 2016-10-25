using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;


namespace CodeMash.Net.DataContracts
{
    public class FindResponse : ResponseBase<string>
    {
        public string Schema { get; set; }
#if DEBUG

        [BsonElement("filter")]
        [DataMember(Name="filter")]
        public string Filter { get; set; }

        [BsonElement("sort")]
        [DataMember(Name = "sort")]
        public string Sort { get; set; }

        [BsonElement("projection")]
        [DataMember(Name = "projection")]
        public string Projection { get; set; }

#endif

    }
}