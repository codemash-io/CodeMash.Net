using System.Runtime.Serialization;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class Count : RequestBase
    {

        [DataMember]
        public string Filter { get; set; }

        [DataMember]
        public CountOptions CountOptions { get; set; }
    }
}