using System.Runtime.Serialization;

namespace CodeMash
{
    /// <summary>
    /// Base class for all client request messages of the web service. It standardizes 
    /// communication between web services and clients with a series of common values.
    /// Derived request message classes assign values to these variables. There are no 
    /// default values. 
    /// </summary>
    [DataContract(Namespace = "http://codemash.io/types/")]
    public abstract class RequestBase  /*: ICultureBasedRequest */
    {
        protected RequestBase()
        {
#if NET452
            CultureCode = Thread.CurrentThread.CurrentCulture.Name;
#endif
        }

        [DataMember]
        public string CultureCode { get; set; }

    }
}