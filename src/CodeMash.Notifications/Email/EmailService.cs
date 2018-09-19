using System;
using System.Collections.Generic;
using CodeMash.Interfaces;

namespace CodeMash.Notifications.Email
{
    public class EmailService : IEmailService
    {
        public ICodeMashSettings CodeMashSettings { get; set; }
        
        public bool SendMail(string[] recipients, string templateName, Dictionary<string, object> tokens, Guid? accountId)
        {
            // TODO : use FluentValidation instead
            if (recipients == null)
            {
                throw new ArgumentNullException();
            }
            
            var response = CodeMashSettings.Client.Post<SendMailResponse>(new SendMail
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