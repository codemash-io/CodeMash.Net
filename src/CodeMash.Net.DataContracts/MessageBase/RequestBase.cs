using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    /// <summary>
    /// Base class for all client request messages of the web service. It standardizes 
    /// communication between web services and clients with a series of common values.
    /// Derived request message classes assign values to these variables. There are no 
    /// default values. 
    /// </summary>
    [DataContract(Namespace = "http://www.CodeMash.com/types/")]
    public abstract class RequestBase : ICultureBasedRequest
    {
        protected RequestBase()
        {
            OutputMode = JsonOutputMode.Strict;
        }

        [DataMember]
        public string CultureCode { get; set; }
        
        [DataMember]
        public string CollectionName { get; set; }

        [DataMember]
        public JsonOutputMode OutputMode { get; set; }

    }

}