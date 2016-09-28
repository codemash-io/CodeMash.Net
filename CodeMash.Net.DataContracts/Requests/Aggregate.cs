using System.Runtime.Serialization;
using MongoDB.Driver;

namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class Aggregate : RequestBase
    {
        public Aggregate() { }

        public Aggregate(string collectionName)
        {
            CollectionName = collectionName;
        }
        
        [DataMember]
        public string Aggregation { get; set; }

        [DataMember]
        public AggregateOptions AggregateOptions { get; set; }
    }
}