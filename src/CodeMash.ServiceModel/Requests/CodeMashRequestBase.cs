using System;
using System.Runtime.Serialization;

namespace CodeMash.ServiceModel
{
    public class CodeMashRequestBase : RequestBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }
    }
}