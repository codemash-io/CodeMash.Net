using System;
using System.Collections.Generic;
using CodeMash.Interfaces;

namespace CodeMash.Notifications.Email
{
    public class EmailService : IEmailService
    {
        public ICodeMashSettings CodeMashSettings { get; set; }
        
        public bool SendMail(string[] recipients, string templateName, Dictionary<string, object> tokens = null, Guid? accountId = null)
        {
            // TODO : use FluentValidation instead
            if (recipients == null)
            {
                throw new ArgumentNullException();
            }

            if (CodeMashSettings == null)
            {
                throw new ArgumentNullException("CodeMashSettings", "CodeMash settings is not set");
            }
            
            var response = CodeMashSettings.Client.Post<SendEmailResponse>("/email/send", new SendEmail
            {
                Recipients = recipients,
                TemplateName = templateName,
                Tokens = tokens,
                AccountId = accountId
            });

            return response != null && response.Result;
        }
    }
}