using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class InsertMany : RequestBase
    {
        /// <summary>
        /// Entity itself should have SchemaId
        /// SchemaId allows to application know how to display entity
        /// </summary>
        [DataMember]
        public IEnumerable<string> Documents { get; set; }

        /// <summary>
        /// The corresponding option in the new API is the IsOrdered flag in the InsertManyOptions class. When the server executes inserts in order it stops at the first error. When the server is allowed to execute the inserts in any order, it does not stop when an error occurs.
        /// </summary>
        /// <value>The insert many options.</value>
        [DataMember]
        public InsertManyOptions InsertManyOptions { get; set; }

        [DataMember]
        public Notification Notification { get; set; }
    }
}