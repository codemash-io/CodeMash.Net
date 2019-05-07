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
    [Api("Creates push notification which will be sent to the device of recipient(s)")]
    [Route("/notifications/push", "POST", Summary = "Creates push notification which will be sent to the device of recipient(s)")]
    public class CreateNotification : CodeMashRequestBase
    {
        [DataMember]
        [ApiMember(DataType = "NotificationSound", Description = @"Notification sound (None = 0, Default = 1).", IsRequired = false, Name = "Sound", ParameterType = "form")]
        public NotificationSound Sound { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "NotificationPriority", Description = @"Notification priority (None = 0, Default = 1, Normal = 2, High = 3", IsRequired = false, Name = "Priority", ParameterType = "form")]
        public NotificationPriority Priority { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "string", Description = @"Title of notification", IsRequired = true, Name = "Title", ParameterType = "form")]

        public string Title { get; set; }
        
        [DataMember]
        [ApiMember(DataType = "string", Description = @"Body of notification", IsRequired = true, Name = "Body", ParameterType = "body")]
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
        
        [DataMember]
        [ApiMember(DataType = "bool", Description = @"If true, then this notification cannot bet pushed.", IsRequired = false, Name = "IsStatic", ParameterType = "body")]
        public bool IsStatic { get; set; }
    }
}