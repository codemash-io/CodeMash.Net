using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using ServiceStack;

namespace CodeMash.ServiceModel
{
    /// <summary>
    /// Base class for all response messages to clients of the web service. It standardizes 
    /// communication between web services and clients with a series of common values and 
    /// their initial defaults. Derived response message classes can override the default 
    /// values if necessary.
    /// </summary>
    [DataContract(Namespace = "http://www.CodeMash.com/types/")]
    public class ResponseBase<T> : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        protected ResponseBase()
        {
        }

        protected ResponseBase(T result)
        {
            Result = result;
        }

        [BsonElement("result")]
        [DataMember(Name="result")]

        public T Result { get; set; }

    }
}