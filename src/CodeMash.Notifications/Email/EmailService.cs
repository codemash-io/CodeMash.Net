using System;
using CodeMash.Interfaces;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Notifications.Email
{
    public class EmailService : IEmailService
    {
        private void AssertItHasSettings()
        {
            if (CodeMashSettings == null)
            {
                throw new ArgumentNullException(nameof(CodeMashSettings), "CodeMash settings is not set");
            }  
        }
        
        public ICodeMashSettings CodeMashSettings { get; set; }
        
        public SendEmailResponse SendMail(SendEmail email)
        {
            AssertItHasSettings();
            
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email), "No email information defined");
            }
            
            if (email.Recipients == null)
            {
                throw new ArgumentNullException(nameof(email.Recipients), "No recipients defined");
            }

            return CodeMashSettings.Client.Post<SendEmailResponse>(email);
        }
    }
}