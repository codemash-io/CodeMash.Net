using System.Runtime.Serialization;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class DeleteMany : RequestBase
    {
        [DataMember]
        public Notification Notification { get; set; }

        [DataMember]
        public string Filter { get; set; }
    }
}