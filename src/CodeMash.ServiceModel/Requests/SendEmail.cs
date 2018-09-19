using System;
using System.Collections.Generic;
using CodeMash.ServiceModel;

namespace CodeMash.Notifications.Email
{
    public class SendEmail : RequestBase
    { 

        public string[] Recipients { get; set; }

        public string TemplateName { get; set; }

        public Dictionary<string, object> Tokens { get; set; }

        public Guid? AccountId{ get; set; }
    }
}