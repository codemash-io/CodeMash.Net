using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;

namespace CodeMash.ServiceModel.Requests.Notifications.Push
{
    [DataContract]
    public class CreateNotificationDevice : CodeMashRequestBase
    {
        [DataMember]
        [ApiMember(DataType = "Guid", Description = @"UserId. Parameter can be nullable, but if you provide it, device will be combined with the one of membership users.", IsRequired = false, Name = "UserId", ParameterType = "form")]
        public Guid? UserId { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "string", Description = @"In which time zone device is registered. If we are aware of location, we can provide notifications in right time frame.", IsRequired = false, Name = "TimeZone", ParameterType = "form")]
        public string TimeZone { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "Dictionary", Description = @"Meta information that comes from device.", IsRequired = false, Name = "Meta", ParameterType = "body")]
        public Dictionary<string, string> Meta { get; set; }
    }
}