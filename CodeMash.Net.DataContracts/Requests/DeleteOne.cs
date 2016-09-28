using System.Runtime.Serialization;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class DeleteOne : RequestBase
    {
        [DataMember]
        public Notification Notification { get; set; }

        [DataMember]
        public string Filter { get; set; }
    }
}