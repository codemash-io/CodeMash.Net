using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace CodeMash.Net.DataContracts
{
    [DataContract]
    public class Notification
    {
        public Notification()
        {
            RecipientsChannels = "CodeMash";
            NotificationLevel = NotificationLevel.NotSet;
        }
        /// <summary>
        /// Message to the end user
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Specify template name of message. 
        /// </summary>
        /// <value>The name of the template unique.</value>
        [DataMember]
        public string TemplateUniqueName { get; set; }

        /// <summary>
        /// If template is set (TemplateUniqueName is provided), please set TemplateDataAsJson property with template data. http://CodeMash.com/documentation/api/notifications/templates
        /// </summary>
        /// <value>The template data.</value>
        [DataMember]
        public string TemplateDataAsJson { get; set; }

        /// <summary>
        /// Recipients' channels. e.g.: "email|sms|apple|CodeMash" - sends notification as email, sms, IPhone app notification and to the CodeMash dashboard
        /// </summary>
        /// <value>Distribution channels : email, sms, call, phone, CodeMash</value>
        [DataMember]
        public string RecipientsChannels { get; set; }

        /// <summary>
        /// When Recipients Channels are set, you need provide either <see cref="TargetAudience">TargetAudience</see> or this property in json array format. e.g.: [{ email : [ 'domantas.jovaisas@gmail.com', 'dj@gmail.com']}, { sms : ['+37065012345']}]
        /// </summary>
        /// <value>Recipients data as json. Key property should be equal to RecipientsChannels item</value>
        [DataMember]
        public string RecipientsDataAsJson { get; set; }
        
        /// <summary>
        /// Target audience to get notification. Provide collection and query as json.
        /// </summary>
        /// <value>The target.</value>
        [DataMember]
        public NotificationQuery TargetAudience { get; set; }
        
        /// <summary>
        /// Gets or sets the notification level. (Error, Info, Warning, NotSet)
        /// </summary>
        /// <value>The notification level.</value>
        [DataMember]
        public NotificationLevel NotificationLevel { get; set; }

        /// <summary>
        /// Set meta information as json for a reference
        /// </summary>
        /// <value>The data.</value>
        [DataMember]
        public string Data { get; set; }

    }

    public enum NotificationLevel
    {
        NotSet,
        Info, 
        Warning,
        Error
    }
}
