using System.Runtime.Serialization;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class FindOne : RequestBase
    {
        [DataMember]
        public bool IncludeSchema { get; set; }
       
        [DataMember]
        public string Filter { get; set; }

        [DataMember]
        public FindOptions FindOptions { get; set; }

        [DataMember]
        public string Projection { get; set; }
    }
}