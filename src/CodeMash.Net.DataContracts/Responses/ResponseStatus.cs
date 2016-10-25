using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class ResponseStatus
    {

        [DataMember(Order = 1)]
        public string ErrorCode { get; set; }
        [DataMember(Order = 4)]
        public List<ResponseError> Errors { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }
        [DataMember(Order = 5)]
        public Dictionary<string, string> Meta { get; set; }
        [DataMember(Order = 3)]
        public string StackTrace { get; set; }
    }

    public class ResponseError 
    {

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
        public string ErrorCode { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
        public string FieldName { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public string Message { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public Dictionary<string, string> Meta { get; set; }
    }
}
