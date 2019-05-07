using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;

namespace CodeMash.ServiceModel.Requests.Notifications.Push
{
    public enum NotificationSound
    {
        None = 0,
        Default = 1
    }
    
    public enum NotificationPriority
    {
        None = 0,
        Default = 1,
        Normal = 2,
        High = 3
    }
    
    [DataContract]
    public class CreateNotification : CodeMashRequestBase
    {
        [DataMember]
        public NotificationSound Sound { get; set; }
        
        [DataMember]
        public NotificationPriority Priority { get; set; }
        
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public string Body { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "object", Description = @"Custom data for your notification", IsRequired = false, Name = "Data", ParameterType = "body")]
        public object Data { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "int", Description = @"Ttl - time in seconds.", IsRequired = false, Name = "Ttl", ParameterType = "form")]
        public int? Ttl { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "int", Description = @"Expiration", IsRequired = false, Name = "Expiration", ParameterType = "form")]
        public int? Expiration { get; set; }

        [DataMember]
        [ApiMember(DataType = "string", Description = @"Subtitle - IOS only", IsRequired = false, Name = "Subtitle", ParameterType = "form")]
        public string Subtitle { get; set; }

        [DataMember]
        [ApiMember(DataType = "int", Description = @"Badge", IsRequired = false, Name = "Badge", ParameterType = "form")]
        public int? Badge { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "string", Description = @"Category", IsRequired = false, Name = "Category", ParameterType = "form")]
        public string Category { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "string", Description = @"ChannelId - Android only", IsRequired = false, Name = "ChannelId", ParameterType = "form")]
        public string ChannelId { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "Dictionary", Description = @"Meta", IsRequired = false, Name = "Meta", ParameterType = "body")]
        public Dictionary<string, string> Meta { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "List", Description = @"Recipients list. You can send messages by specifying CodeMash membership users which are combined with devices.", IsRequired = false, Name = "Users", ParameterType = "body")]
        public List<Guid?> Users { get; set; }   // Gets devices by user ids
        
        [DataMember]
        [ApiMember(DataType = "List", Description = @"Messages to be delivered into specified devices.", IsRequired = false, Name = "Devices", ParameterType = "body")]

        public List<Guid> Devices { get; set; } // Gets devices by device ids
    }
}