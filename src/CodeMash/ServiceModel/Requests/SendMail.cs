using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeMash.ServiceModel
{
    [DataContract]
    public class SendMail : RequestBase
    { 
        [DataMember]
        public string To { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        [DataMember]
        public string ModelInJsonAsString { get; set; }

        [DataMember]
        public string From { get; set; }

        [DataMember]
        public List<MailAttachmentDataContract> Attachments { get; set; }

    }
}