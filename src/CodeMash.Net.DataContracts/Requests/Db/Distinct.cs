using System.Runtime.Serialization;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class Distinct : RequestBase
    {

        [DataMember]
        public string Filter { get; set; }

        [DataMember]
        public string Field { get; set; }


        [DataMember]
        public DistinctOptions DistinctOptions { get; set; }
    }
}