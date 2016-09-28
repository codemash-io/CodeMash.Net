using System.Reflection;
using System.Runtime.Serialization;

using MongoDB.Bson.Serialization.Attributes;

namespace CodeMash.Net.DataContracts
{
    /// <summary>
    /// Base class for all response messages to clients of the web service. It standardizes 
    /// communication between web services and clients with a series of common values and 
    /// their initial defaults. Derived response message classes can override the default 
    /// values if necessary.
    /// </summary>
    [DataContract(Namespace = "http://www.CodeMash.com/types/")]
    public class ResponseBase<T> 
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

        /// <summary>
        /// Version number (in major.minor format) of currently executing web service. 
        /// Used to offer a level of understanding (related to compatibility issues) between
        /// the client and the web service as the web services evolve over time. 
        /// Ebay.com uses this in their API.
        /// </summary>
        [DataMember(Name = "version")]
        [BsonElement("version")]
        public string Version =
            Assembly.GetExecutingAssembly().GetName().Version.Major + "." +
            Assembly.GetExecutingAssembly().GetName().Version.Minor;

        /// <summary>
        /// Build number of currently executing web service. Used as an indicator
        /// to client whether certain code fixes are included or not.
        /// Ebay.com uses this in their API.
        /// </summary>
        [DataMember(Name = "build")]
        [BsonElement("build")]
        public string Build =
            Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();

    }
}