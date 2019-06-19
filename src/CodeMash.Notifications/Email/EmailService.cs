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
        
        public SendPushNotificationResponse SendMail(SendEmail email)
        {
            AssertItHasSettings();
            
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email), "No email information defined");
            }
            
            if (email.Emails == null && (email.Users == null || email.Users.Count == 0))
            {
                throw new ArgumentNullException("No recipients defined");
            }

            return CodeMashSettings.Client.Post(email);
        }
    }
}